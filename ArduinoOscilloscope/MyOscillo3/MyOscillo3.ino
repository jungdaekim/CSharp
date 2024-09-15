int analogPin=A0;
String receiveString;

int numBurstSamples=500;
//unsigned long timeStart;
//unsigned long timeStop;

#define cbi(sfr,bit) (_SFR_BYTE(sfr)&=~_BV(bit))
#define sbi(sfr,bit) (_SFR_BYTE(sfr)|=_BV(bit))

// In C#, and "int" is 32 bits, but in Arduino it is 16 bits.
// So we need to use an unsigned long to capture entire 32 bits we may receive.
// But since the burst duration in mSec likely won't exceed 1000 mSec,
// we could also use only a "ushort" in C#, which is a 0~65,535 value.
unsigned long burstDurationmSec=0;

// This sketch first waits for a "Sxxx" command from a C# app, over the USE, and
// when it receives the command (which includes the number of samples to take each burst),
// it sets the "numBurstSamples" variable to be used for subsequent burst requests.
// It then waits for a "Bxxx" command, which includes the total burst duration in mSec.
// It then calculates how much to delay each sample so it can fit all the samples
// in the desired burst duration. It then does a series of analogRead() functions
// on the analog input pin, and saves the 0~1023 values into a one-dimensional array named "val".
// It then immediately sends the contents of that filled array 
// over the USB as strings (eq., "1023 \r\n").

void setup() {
  // put your setup code here, to run once:
  Serial.begin(2000000);
  pinMode(analogPin, INPUT);
  sbi(ADCSRA, ADPS2);
  cbi(ADCSRA, ADPS1);
  cbi(ADCSRA, ADPS0);
}

void loop() {
  // Check to see if the PC has sent a command telling the Arduino to return data
  if(Serial.available())
  {
    receiveString=Serial.readStringUntil('\n');

    // Determine if the computer has sent a string telling the Arduino how may samples
    // to grab each burst. A default of 500 is already set, and this only updates
    // the requested number of samples if the user decides to change the sample rate.
    if(receiveString.startsWith("S"))
    {
      numBurstSamples=receiveString.substring(1).toInt();
    }
    // Otherwise, if the computer has sent a string telling the Arduino the desired
    // mSec length of the burst...
    else if(receiveString.startsWith("B"))
    {
      burstDurationmSec=receiveString.substring(1).toInt();
      GrabBurstandSend();
    }
  }
}

void GrabBurstandSend()
{
  unsigned int val[numBurstSamples]; // 2 bytes per unsigned int
  unsigned long burstSampleDelayuSec=((1000*(burstDurationmSec))/numBurstSamples)-100;

  while(analogRead(analogPin)<500 || analogRead(analogPin)>510)
  {
  }
  // Read numSamples and fill arrays
  for(int i=0;i<numBurstSamples;i++)
  {
    val[i]=analogRead(analogPin);
    delayMicroseconds(burstSampleDelayuSec);
  }
  for(int i=0;i<numBurstSamples;i++)
  {
    Serial.println(val[i]);
  }
  Serial.println("END");
}


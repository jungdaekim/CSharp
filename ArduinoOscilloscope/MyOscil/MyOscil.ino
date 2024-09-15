int analogPin=A0;
String receiveString;

int numBurstSamples;
unsigned long timeStart;
unsigned long timeStop;
unsigned long timeElapsed;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(2000000);
  pinMode(analogPin, INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available())
  {
    receiveString=Serial.readString();
    if(receiveString.startsWith("B"))
    {
      timeStart=micros();
      numBurstSamples=receiveString.substring(1).toInt();
      GrabBurstandSend(numBurstSamples);
    }
    timeStop=micros();
    timeElapsed=timeStop-timeStart;
    Serial.print("Elapsed uSec = ");
    Serial.println(timeElapsed);
  }
}

void GrabBurstandSend(int numSamples)
{
  unsigned int val[numSamples]; // 2 bytes per int
  unsigned long time[numSamples];  // 4 bytes per long

  for(int i=0;i<numSamples;i++)
  {
    time[i]=micros();
    val[i]=analogRead(analogPin);
  }
  for(int i=0;i<numSamples;i++)
  {
    Serial.print(time[i]);
    Serial.print(" , ");
    Serial.println(val[i]);
  }
  Serial.println("END");
}

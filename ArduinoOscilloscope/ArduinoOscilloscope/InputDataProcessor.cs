using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoOscilloscope
{
    public class InputDataProcessor
    {
        #region DOCS

        #endregion

        #region TODO

        #endregion

        #region Properties
        public List<string> inputStringList { get; set; }
        public int[] inputIntArray { get; set; }
        public double[,] processedDoubleArray {  get; set; }    
        public string[] processedStringArray { get; set; }
        public double burstAverage;

        public double startTime;
        public double endTime;
        public double freq;
        #endregion

        #region Constructor
        public InputDataProcessor(List<string> InputStringList)
        {
            this.inputIntArray=new int[InputStringList.Count];
            this.processedDoubleArray = new double[InputStringList.Count, 2];
            this.processedStringArray = new string[InputStringList.Count];
            this.inputStringList= InputStringList;
        }
        #endregion

        #region Methods
        public void ParseCSVBurst()
        {
            int i = 0;
            foreach (string s in this.inputStringList)
            {
                string valString = s.TrimEnd('\r', '\n');

                if(int.TryParse(valString, out this.inputIntArray[i])) { }
                else
                {
                    this.inputIntArray[i] = 0;
                }

                i++;
            }
        }

        public void ScaleValuesBurst(int scaleRange, double Vref)
        {
            // Convert max value 1023 to double
            double rangeDouble = Convert.ToDouble(scaleRange);

            for(int i=0;i<this.inputIntArray.Length;i++)
            {
                // Convert the sample integer value (range 0-1023) to double
                double sampleDouble=Convert.ToDouble(this.inputIntArray[i]);

                // Multiply ratio of sample value to max value times max voltage(mag)
                this.processedDoubleArray[i, 1] = Vref * sampleDouble / rangeDouble;
            }
        }

        public void ZeroTimesBurst(double sampleIntmSec)
        {
            double sampleTime = 0.0;

            // Generate zero-based sample times based on timer interval setting
            for(int i=0;i<this.inputIntArray.Length;i++)
            {
                this.processedDoubleArray[i, 0] = sampleTime;
                sampleTime = sampleTime + sampleIntmSec;
            }
        }

        public double GetMax()
        {
            double voltsMax = 0.0;

            for(int i=0;i<this.inputIntArray.Length;i++)
            {
                if (this.processedDoubleArray[i,1]>voltsMax)
                {
                    voltsMax = this.processedDoubleArray[i, 1];
                }
            }
            return voltsMax;
        }

        public double GetAverage()
        {
            double ArraySum = 0.0;
            //Cycle through each value element of the processedDoubleArray to get a sum
            for(int i=0;i<this.inputIntArray.Length ;i++)
            {
                ArraySum = ArraySum + processedDoubleArray[i, 1];
            }
            return burstAverage = ArraySum / Convert.ToDouble(inputIntArray.Length);
        }

        public void GetACCoupled()
        {
            GetAverage();

            for(int i=0;i<this.inputIntArray.Length;i++)
            {
                processedDoubleArray[i, 1] = processedDoubleArray[i,1]-burstAverage;
            }

        }


        public double GetFreq()
        {
            bool startDetected = false;

            for (int i = 1; i < inputIntArray.Length; i++)
            {
                if (processedDoubleArray[i-1,1]<0 &&
                    processedDoubleArray[i,1]>0)
                {
                    if(startDetected==false)
                    {
                        startTime = processedDoubleArray[i, 0];
                        startDetected = true;
                    }
                    else if(startDetected==true)
                    {
                        endTime = processedDoubleArray[i, 0];
                        freq = 1000.0 / (endTime - startTime);
                        return freq;
                    }
                }
            }
            return freq;
        }

        //public string[] GetCSVOutput()
        //{

        //}


        #endregion

    }
}

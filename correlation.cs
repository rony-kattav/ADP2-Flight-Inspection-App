using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    class correlation
    {

        //############### for the graphs!!

        // returns an array of lists - each list is a coloumn from the csv.
        private List<double>[] create2Darray()
        {
            string[] splitline = dataArray[0].Split(',');
            int numOfCols = splitline.Length;
            double[,] array2D = new double[numofrows, numOfCols];
            for (int i = 0; i < numOfRows; i++)
            {
                splitline = dataArray[i].Split(',');
                for (int j = 0; j < numOfCols; j++)
                {
                    array2D[i, j] = Double.Parse(splitline[j]);
                }
            }
            return toList(array2D, numOfCols, numofrows);
        }

        private List<double>[] toList(double[,] arr, int cols, int rows)
        {
            List<double>[] listArr = new List<double>[cols];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    listArr[i].Add(arr[j, i]);
                }
            }
            return listArr;
        }

        private Dictionary<int, int> findBestCor()
        {
            Dictionary<int, int> correlations = new Dictionary<int, int>();
            List<double>[] array = create2Darray();
            int numofcols = array.Length;
            for (int i = 0; i < numofcols; i++)
            {
                double maxcor = 0;
                int maxcorcol = -1;
                for (int j = 0; j < numofcols; j++)
                {
                    if (i != j)
                    {
                        var avg1 = array[i].Average();
                        var avg2 = array[j].Average();

                        var sum1 = array[i].Zip(array[j], (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

                        var sumSqr1 = array[i].Sum(x => Math.Pow((x - avg1), 2.0));
                        var sumSqr2 = array[j].Sum(y => Math.Pow((y - avg2), 2.0));

                        var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);
                        if (result > maxcor)
                        {
                            maxcorcol = j;
                            maxcor = result;
                        }
                    }

                }
                correlations.Add(i, maxcorcol);
            }
            return correlations;
        }

        //############### for the graphs!!
    }
}

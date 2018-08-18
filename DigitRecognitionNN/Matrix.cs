using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitRecognitionNN
{
    class Matrix<T>
    {
        public T[,] Multiply(T[,] m1, T[,] m2)
        {
            if (m1.GetUpperBound(1) != m2.GetUpperBound(0)) throw new Exception("Multiplied matrixes has wrong dimensions");
            int rows = m1.GetUpperBound(0) + 1,            
                cols_m1_rows_m2 = m1.Length / rows,
                cols_m2 = m2.GetUpperBound(1) + 1;

            T[,] result = new T[rows, cols_m2];

            for (int i = 0; i < rows; i++) 
                for (int j = 0; j < cols_m2; j++)
                    for(int k = 0; k < cols_m1_rows_m2; k++)                    
                        result[i, j] += (dynamic)m1[i, k] * m2[k, j];
            return result;
        }

        public T[,] Subtract(T[,] m1, T[,] m2)
        {
            if (m1.Length != m2.Length || m1.GetUpperBound(0) != m2.GetUpperBound(0))
                throw new Exception("Substractable matrixes has wrong dimensions");            
            int rows = m1.GetUpperBound(0) + 1,
                cols = m1.GetUpperBound(1) + 1;
            T[,] result = new T[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i,j] = (dynamic)m1[i, j] - (dynamic)m2[i, j];
            return result;
        }

        public T[,] Transpose(T[,] m)
        {
            int rows = m.GetUpperBound(0) + 1,
                cols = m.GetUpperBound(1) + 1;
            T[,] result = new T[cols, rows];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[j, i] = m[i, j];
            return result;
        }
    }
}

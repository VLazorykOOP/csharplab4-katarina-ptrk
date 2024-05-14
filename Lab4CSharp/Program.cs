using System;

class Date
{
    private int day;
    private int month;
    private int year;

    public Date(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    public int Day
    {
        get { return day; }
        set
        {
            if (value >= 1 && value <= 31)
                day = value;
            else
                throw new ArgumentException("Invalid day value.");
        }
    }

    public int Month
    {
        get { return month; }
        set
        {
            if (value >= 1 && value <= 12)
                month = value;
            else
                throw new ArgumentException("Invalid month value.");
        }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    public void PrintDate()
    {
        string[] months = {"січня", "лютого", "березня", "квітня", "травня", "червня",
                           "липня", "серпня", "вересня", "жовтня", "листопада", "грудня"};
        Console.WriteLine($"{Day} {months[Month - 1]} {Year} року");
    }

    public void PrintDateFormatted()
    {
        Console.WriteLine($"{Day:D2}.{Month:D2}.{Year}");
    }

    public bool IsValidDate()
    {
        if (Year < 1)
            return false;

        if (Month < 1 || Month > 12)
            return false;

        if (Day < 1)
            return false;

        int[] daysInMonth = {31, 28 + (IsLeapYear(Year) ? 1 : 0), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        return Day <= daysInMonth[Month - 1];
    }

    private bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public int DifferenceInDays(Date other)
    {
        DateTime date1 = new DateTime(Year, Month, Day);
        DateTime date2 = new DateTime(other.Year, other.Month, other.Day);
        TimeSpan difference = date2 - date1;
        return Math.Abs(difference.Days);
    }

    public int Century
    {
        get { return (Year - 1) / 100 + 1; }
    }

    public Date this[int index]
    {
        get
        {
            DateTime current = new DateTime(Year, Month, Day);
            if (index >= 0)
                return new Date(current.AddDays(index).Day, current.AddDays(index).Month, current.AddDays(index).Year);
            else
                return new Date(current.AddDays(-index).Day, current.AddDays(-index).Month, current.AddDays(-index).Year);
        }
    }

    public static bool operator !(Date date)
    {
        return date.Day != DateTime.DaysInMonth(date.Year, date.Month);
    }

    public static bool operator true(Date date)
    {
        return date.Day == 1 && date.Month == 1;
    }

    public static bool operator false(Date date)
    {
        return !(date.Day == 1 && date.Month == 1);
    }

    public static bool operator &(Date date1, Date date2)
    {
        return date1.Day == date2.Day && date1.Month == date2.Month && date1.Year == date2.Year;
    }

    public static implicit operator string(Date date)
    {
        return $"{date.Day:D2}.{date.Month:D2}.{date.Year}";
    }

    public static implicit operator Date(string dateStr)
    {
        string[] parts = dateStr.Split('.');
        if (parts.Length != 3)
            throw new ArgumentException("Invalid date format.");

        int day = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int year = int.Parse(parts[2]);

        return new Date(day, month, year);
    }


}


//task2
class VectorByte
{
    private byte[] ByteArray;
    private uint size;
    private int codeError;
    private static uint num_vec;

    public VectorByte()
    {
        ByteArray = new byte[1];
        ByteArray[0] = 0;
        size = 1;
        codeError = 0;
        num_vec++;
    }

    public VectorByte(uint size)
    {
        ByteArray = new byte[size];
        for (var i = 0; i < size; i++)
        {
            ByteArray[i] = 0;
        }

        this.size = size;
        num_vec++;
        codeError = 0;
    }

    public VectorByte(uint size, byte num)
    {
        ByteArray = new byte[size];
        for (var i = 0; i < size; i++)
        {
            ByteArray[i] = num;
        }

        this.size = size;
        num_vec++;
        codeError = 0;
    }

    ~VectorByte()
    {
        Console.WriteLine("Destructor");
    }

    public void inputArr()
    {
        for (var i = 0; i < size; i++)
        {
            byte.TryParse(Console.ReadLine(), out ByteArray[i]);
        }
    }

    public void printArr()
    {
        for (var i = 0; i < size; i++)
        {
            Console.Write($"{ByteArray[i]} ");
        }
        Console.WriteLine();
    }

    public void setArr(byte num)
    {
        for (var i = 0; i < size; i++)
        {
            ByteArray[i] = num;
        }
    }

    public uint getSize()
    {
        return size;
    }
    public byte this[uint index]
    {
        get
        {
            if (index > size)
            {
                codeError = -1;
                return 0;
            }
            return ByteArray[index];
        }
        set
        {
            if (index > size)
            {
                codeError = -1;
            }
            else
            {
                ByteArray[index] = value;
            }
        }
    }
    public static VectorByte operator ++(VectorByte vectorByte)
    {
        for (var i = 0; i < vectorByte.size; i++)
        {
            vectorByte.ByteArray[i]++;
        }
        return vectorByte;
    }

    public static VectorByte operator --(VectorByte vectorByte)
    {
        for (var i = 0; i < vectorByte.size; i++)
        {
            vectorByte.ByteArray[i]--;
        }
        return vectorByte;
    }
    public static bool operator true(VectorByte vectorByte)
    {
        if (vectorByte.size != 0)
        {
            return true;
        }
        return false;
    }
    public static bool operator false(VectorByte vectorByte)
    {
        if (vectorByte.size != 0)
        {
            return false;
        }
        return true;
    }
    public static VectorByte operator +(VectorByte vectorByte, byte num)
    {
        for (var i = 0; i < vectorByte.size; i++)
        {
            vectorByte.ByteArray[i] = Convert.ToByte(vectorByte.ByteArray[i] + num);
        }
        return vectorByte;
    }

    public static VectorByte operator +(VectorByte a, VectorByte b)
    {
        uint lessSize = a.size < b.size ? a.size : b.size;
        for (var i = 0; i < lessSize; i++)
        {
            a.ByteArray[i] += b.ByteArray[i];
        }
        return a;
    }
    public static VectorByte operator -(VectorByte vectorByte, byte num)
    {
        for (var i = 0; i < vectorByte.size; i++)
        {
            vectorByte.ByteArray[i] = Convert.ToByte(vectorByte.ByteArray[i] - num);
        }
        return vectorByte;
    }

    public static VectorByte operator -(VectorByte a, VectorByte b)
    {
        uint lessSize = a.size < b.size ? a.size : b.size;
        for (var i = 0; i < lessSize; i++)
        {
            a.ByteArray[i] -= b.ByteArray[i];
        }
        return a;
    }
    public static VectorByte operator *(VectorByte vectorByte, int num)
    {
        for (var i = 0; i < vectorByte.size; i++)
        {
            vectorByte.ByteArray[i] = Convert.ToByte(vectorByte.ByteArray[i] * num);
        }
        return vectorByte;
    }

    public static VectorByte operator *(VectorByte a, VectorByte b)
    {
        uint lessSize = a.size < b.size ? a.size : b.size;
        for (var i = 0; i < lessSize; i++)
        {
            a.ByteArray[i] *= b.ByteArray[i];
        }
        return a;
    }
    public static VectorByte operator /(VectorByte vectorByte, int num)
    {
        for (var i = 0; i < vectorByte.size; i++)
        {
            vectorByte.ByteArray[i] = Convert.ToByte(vectorByte.ByteArray[i] / num);
        }
        return vectorByte;
    }

    public static VectorByte operator /(VectorByte a, VectorByte b)
    {
        uint lessSize = a.size < b.size ? a.size : b.size;
        for (var i = 0; i < lessSize; i++)
        {
            a.ByteArray[i] /= b.ByteArray[i];
        }
        return a;
    }

}

class MatrixByte
{
    private byte[,] ByteArray;
    private uint n,m;
    private int codeError;
    private static uint num_m;

    public MatrixByte()
    {
        ByteArray = new byte[1,1];
        ByteArray[0,0] = 0;
        n = 1;
        m = 1;
        codeError = 0;
        num_m++;
    }

    public MatrixByte(uint n, uint m)
    {
        ByteArray = new byte[n, m];
        for (var i = 0; i < n; i++)
        {
            for (var c = 0; c < m; c++)
            {
                ByteArray[i, c] = 0;
            }
        }
        this.n = n;
        this.m = m;
        num_m++;
        codeError = 0;
    }

    public MatrixByte(uint n, uint m, byte num)
    {
        ByteArray = new byte[n, m];
        for (var i = 0; i < n; i++)
        {
            for (var c = 0; c < m; c++)
            {
                ByteArray[i, c] = num;
            }
        }
        this.n = n;
        this.m = m;
        num_m++;
        codeError = 0;
    }

    ~MatrixByte()
    {
        Console.WriteLine("Destructor");
    }

    public void inputMat()
    {
        for (var i = 0; i < n; i++)
        {
            for (var c = 0; c < m; c++)
            {
                byte.TryParse(Console.ReadLine(), out ByteArray[i,c]);
            }
        }
    }

    public void PrintMat()
    {
        for (var i = 0; i < n; i++)
        {
                for (var c = 0; c < m; c++)
                {
                    Console.Write($"{ByteArray[i,c]} ");
                }
                Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void SetMat(byte num)
    {
        for (var i = 0; i < n; i++)
        {
            for (var c = 0; c < m; c++)
            {
                ByteArray[i, c] = num;
            }
        }
    }

    public byte this[uint i, uint j]
    {
        get
        {
            if (i > n || j > m)
            {
                codeError = -1;
                return 0;
            }
            return ByteArray[i,j];
        }
        set
        {
            if (i > n || j > m)
            {
                codeError = -1;
            }
            else
            {
                ByteArray[i, j] = value;
            }
        }
    }

    public byte this[int index]
    {
        //rown = n, column = m
        get
        {
            if (index < n * m - 1)
            {

                return ByteArray[index / m, (int)(index % m)];
            }
            else
            {
                codeError = -1;
                return 0;
            }
        }
        set
        {
            if (index < n * m - 1)
            {
                ByteArray[index / m, (int)(index % m)] = value;
            }
            else
            {
                codeError = -1;
            }
        }
    }

    public static MatrixByte operator++(MatrixByte matrixFloat)
    {
        for (var i = 0; i < matrixFloat.n; i++)
        {
            for (var c = 0; c < matrixFloat.m; c++)
            {
                matrixFloat.ByteArray[i, c]++;
            }
        }

        return matrixFloat;
    }

    public static MatrixByte operator--(MatrixByte matrixFloat)
    {
        for (var i = 0; i < matrixFloat.n; i++)
        {
            for (var c = 0; c < matrixFloat.m; c++)
            {
                matrixFloat.ByteArray[i, c]--;
            }
        }
        return matrixFloat;
    }
    public static bool operator true(MatrixByte matrixFloat)
    {
        if(matrixFloat.n != 0 && matrixFloat.m != 0)
        {
            return true;
        }
        return false;
    }
    public static bool operator false(MatrixByte matrixFloat)
    {
        if(matrixFloat.n != 0 && matrixFloat.m != 0)
        {
            return false;
        }
        return true;
    }
    public static MatrixByte operator+(MatrixByte matrixFloat, int num)
    {
        for (var i = 0; i < matrixFloat.n; i++)
        {
            for (var c = 0; c < matrixFloat.m; c++)
            {
                matrixFloat.ByteArray[i, c] = Convert.ToByte(matrixFloat.ByteArray[i, c] + num);
            }
        }

        return matrixFloat;
    }

    public static MatrixByte operator+(MatrixByte a, MatrixByte b)
    {
        uint lessSizeN = a.n < b.n ? a.n : b.n;
        uint lessSizeM = a.m < b.m ? a.m : b.m;
        for (var i = 0; i < lessSizeN; i++)
        {
            for (int c = 0; c < lessSizeM; c++)
            {
                a.ByteArray[i, c] += b.ByteArray[i, c];
            }
        }
        return a;
    }
    public static MatrixByte operator-(MatrixByte matrixFloat, int num)
    {
        for (var i = 0; i < matrixFloat.n; i++)
        {
            for (var c = 0; c < matrixFloat.m; c++)
            {
                matrixFloat.ByteArray[i, c] = Convert.ToByte(matrixFloat.ByteArray[i,c] - num);
            }
        }

        return matrixFloat;
    }

    public static MatrixByte operator-(MatrixByte a, MatrixByte b)
    {
        uint lessSizeN = a.n < b.n ? a.n : b.n;
        uint lessSizeM = a.m < b.m ? a.m : b.m;
        for (var i = 0; i < lessSizeN; i++)
        {
            for (int c = 0; c < lessSizeM; c++)
            {
                a.ByteArray[i, c] -= b.ByteArray[i, c];
            }
        }
        return a;
    }
    public static MatrixByte operator*(MatrixByte matrixUint, int num)
    {
        for (var i = 0; i < matrixUint.n; i++)
        {
            for (var c = 0; c < matrixUint.m; c++)
            {
                matrixUint.ByteArray[i, c] = Convert.ToByte(matrixUint.ByteArray[i,c] * num);
            }
        }
        return matrixUint;
    }

    public static MatrixByte operator*(MatrixByte a, MatrixByte b)
    {
        uint lessSizeN = a.n < b.n ? a.n : b.n;
        uint lessSizeM = a.m < b.m ? a.m : b.m;
        for (var i = 0; i < lessSizeN; i++)
        {
            for (int c = 0; c < lessSizeM; c++)
            {
                a.ByteArray[i, c] *= b.ByteArray[i, c];
            }
        }
        return a;
    }
    public static MatrixByte operator/(MatrixByte matrixUint, int num)
    {
        for (var i = 0; i < matrixUint.n; i++)
        {
            for (var c = 0; c < matrixUint.m; c++)
            {
                matrixUint.ByteArray[i, c] = Convert.ToByte(matrixUint.ByteArray[i, c] / num);
            }
        }

        return matrixUint;
    }

    public static MatrixByte operator/(MatrixByte a, MatrixByte b)
    {
        uint lessSizeN = a.n < b.n ? a.n : b.n;
        uint lessSizeM = a.m < b.m ? a.m : b.m;
        for (var i = 0; i < lessSizeN; i++)
        {
            for (int c = 0; c < lessSizeM; c++)
            {
                a.ByteArray[i, c] /= b.ByteArray[i, c];
            }
        }
        return a;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Date currentDate = new Date(30, 1, 2023);
        Console.WriteLine("Indexer:");
        Console.WriteLine(currentDate[10]);  //prints 11.01.2023
        Console.WriteLine(currentDate[-5]);  //prints 27.12.2022

        Console.WriteLine("\nOverloaded Operators:");
        Console.WriteLine(!currentDate);    //prints True
        Console.WriteLine(currentDate & new Date(30, 1, 2023)); //prints True

        Console.WriteLine("\nImplicit Conversion:");
        Date convertedDate = "25.12.2023";
        Console.WriteLine(convertedDate.Day);      //prints 25
        Console.WriteLine(convertedDate.Month);    //prints 12
        Console.WriteLine(convertedDate.Year);     //prints 2023

        //task2
        var arrA = new VectorByte();
        var arrB = new VectorByte(5, 3);
        Console.WriteLine($"Index[1]: {arrB[1]}");
        Console.WriteLine("Array A: ");
        arrA.printArr();
        Console.WriteLine("Array B: ");
        arrB.printArr();
        arrA++;
        Console.WriteLine("A++: ");
        arrA.printArr();
        arrA--;
        Console.WriteLine("A--: ");
        arrA.printArr();
        Console.WriteLine(arrA ? "Array A exists" : "Array A does not exists");
        Console.WriteLine(arrB ? "Array B exists" : "Array B does not exists");
        Console.WriteLine("Array B: ");
        arrB.printArr();
        arrB = arrB * 2;
        Console.WriteLine("Array B * 2: ");
        arrB.printArr();
        //----
        var matA = new MatrixByte();
        var matB = new MatrixByte(3, 3, 1);
        Console.WriteLine($"Index[1]: {matB[1]}");
        Console.WriteLine("Matrix A: ");
        matA.PrintMat();
        Console.WriteLine("Matrix B: ");
        matB.PrintMat();
        matB++;
        Console.WriteLine("Matrix B++: ");
        matB.PrintMat();
        Console.WriteLine(matA ? "Matrix A exists" : "Matrix A does not exists");
        Console.WriteLine(matB ? "Matrix B exists" : "Matrix B does not exists");
        Console.WriteLine("Matrix B: ");
        matB.PrintMat();
        matB = matB * 2;
        Console.WriteLine("Matrix B * 2: ");
        matB.PrintMat();
    }
}

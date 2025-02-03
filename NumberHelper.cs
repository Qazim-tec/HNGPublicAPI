namespace HngAPI
{
    public static class NumberHelper
    {
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }

        public static bool IsPerfect(int number)
        {
            if (number <= 1) return false;

            int sum = 1;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                    int other = number / i;
                    if (other != i)
                        sum += other;
                }
            }
            return sum == number;
        }

        public static int CalculateDigitSum(int number)
        {
            int sum = 0;
            int n = Math.Abs(number);
            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return sum;
        }

        public static bool IsArmstrongNumber(int number)
        {
            if (number < 0)
                return false;

            int original = number;
            int sum = 0;
            int digitCount = number == 0 ? 1 : (int)Math.Floor(Math.Log10(Math.Abs(number))) + 1;

            int temp = original;
            while (temp != 0)
            {
                int digit = temp % 10;
                sum += (int)Math.Pow(digit, digitCount);
                temp /= 10;
            }

            return sum == original;
        }
    }
}

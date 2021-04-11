class StringUtils
{
    public static bool ContainesDigit(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsDigit(str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainesUpercase(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainesUpercase(string str, int value)
    {
        int counter = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
            {
                counter++;
            }
        }

        if (counter >= value)
            return true;
        else
            return false;
    }

    public static int SecondUpercaseLetter(string str)
    {
        int counter = 0;
        int counter2 = 0;
        for (int i = 0; i < str.Length; i++)
        {
            counter++;
            if (char.IsUpper(str[i]))
            {
                counter2++;
                if (counter2 == 2)
                    return counter;
            }
        }

        return counter;
    }

    public static bool ContainesLower(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsUpper(str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainesLetter(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsLetter(str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ValidEmail(string str)
    {
        if (!str.Contains("@"))
        {
            return false;
        }
        else if (!str.Contains("."))
        {
            return false;
        }
        else if (!StringUtils.ContainesLetter(str))
        {
            return false;
        }

        return true;
    }
}
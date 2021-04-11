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
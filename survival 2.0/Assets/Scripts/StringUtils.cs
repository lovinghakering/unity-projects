class StringUtils
{
    public static bool ContainesDigit(string _str)
    {
        for (int i = 0; i < _str.Length; i++)
        {
            if (char.IsDigit(_str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainesUpercase(string _str)
    {
        for (int i = 0; i < _str.Length; i++)
        {
            if (char.IsUpper(_str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainesUpercase(string _str, int _value)
    {
        int _counter = 0;
        for (int i = 0; i < _str.Length; i++)
        {
            if (char.IsUpper(_str[i]))
            {
                _counter++;
            }
        }

        if (_counter >= _value)
            return true;
        return false;
    }

    public static int SecondUpercaseLetter(string _str)
    {
        int _counter = 0;
        int _counter2 = 0;
        for (int i = 0; i < _str.Length; i++)
        {
            _counter++;
            if (char.IsUpper(_str[i]))
            {
                _counter2++;
                if (_counter2 == 2)
                    return _counter;
            }
        }

        return _counter;
    }

    public static bool ContainesLower(string _str)
    {
        for (int i = 0; i < _str.Length; i++)
        {
            if (char.IsUpper(_str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainesLetter(string _str)
    {
        for (int i = 0; i < _str.Length; i++)
        {
            if (char.IsLetter(_str[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsValidEmail(string _email)
    {
        try {
            var _addr = new System.Net.Mail.MailAddress(_email);
            return _addr.Address == _email;
        }
        catch {
            return false;
        }
    }
}
﻿namespace Common
{
    public interface IYear
    {
        IDay? Day(int day);
        IAnimation? DayAnimation(int day);

    }
}

using System;
using System.Globalization;

namespace IMDBThingy.lib.ValueObjects {

    public class DateOfBirth {

        private DateTime _date;

        public DateOfBirth(int year, int month, int day) {
            _date = new DateTime(year, month, day);

            if (_date.Date > DateTime.Now) {
                throw new Exception("Invalida date of birth!");
            }
        }

        public override string ToString() {
            // use english dateTime.ToString(); if not used, we will get the "born" to be location speceific to Language
            var culture = new CultureInfo("en-US");

            return _date.ToString(culture.DateTimeFormat.ShortDatePattern, culture);
        }

    }

}
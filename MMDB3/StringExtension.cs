using System;
using System.Windows.Markup;

namespace MMDB3 {

    public sealed class StringExtension : MarkupExtension {

        public StringExtension(string value) {
            Value = value;
        }

        public string Value { get; set; }

        public override Object ProvideValue(IServiceProvider sp) {
            return Value;
        }

    };

}
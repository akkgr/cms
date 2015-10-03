using FluentValidation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace cms
{
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IValidator Validator { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");

            MemberExpression body = selectorExpression.Body as MemberExpression;

            if(body == null)
                throw new ArgumentNullException("body");

            OnPropertyChanged(body.Member.Name);
        }

        public string Error
        {
            get
            {
                if (Validator == null)
                    return string.Empty;

                var errors = Validator.Validate(this).Errors;

                if (errors == null || errors.Count == 0)
                    return string.Empty;

                return string.Join(Environment.NewLine, errors.Select(t => t.ErrorMessage));
            }
                
        }

        public string this[string columnName]
        {
            get
            {
                if (Validator == null)
                    return string.Empty;

                var errors = Validator.Validate(this).Errors;

                if (errors == null || errors.Count == 0)
                    return string.Empty;

                var error = errors.FirstOrDefault(t => t.PropertyName == columnName);
                if (error == null)
                    return string.Empty;

                return error.ErrorMessage;
            }
        }
    }
}

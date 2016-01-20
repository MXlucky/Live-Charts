using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace lvc
{
    public class SeriesConfiguration<T> : ISeriesConfiguration
    {
        private int _xIndexer;
        private int _yIndexer;
        public SeriesConfiguration()
        {
            XValueMapper = (value, index) => index;
            OptimizationMethod = values =>
            {
                _xIndexer = 0;
                _yIndexer = 0;
                return values.Select(v => new Point(XValueMapper(v, _xIndexer++), YValueMapper(v, _yIndexer++)));
            };
        }

        /// <summary>
        /// Gets or sets optimization method
        /// </summary>
        internal Func<IEnumerable<T>, IEnumerable<Point>> OptimizationMethod { get; set; }
       
        /// <summary>
        /// Gets or sets the current function that pulls X value from T
        /// </summary>
        private Func<T, int, double> XValueMapper { get; set; }

        /// <summary>
        /// Gets or sets the current function that pulls Y value from T
        /// </summary>
        private Func<T, int, double> YValueMapper { get; set; }

        public Func<double, string> YLabelFormatter { get; internal set; }
        public Func<double, string> XLabelFormatter { get; internal set; }

        private SeriesCollection Collection { get; set; } 

        /// <summary>
        /// Maps X value
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SeriesConfiguration<T> X(Func<T, double> predicate)
        {
            XValueMapper = (x, i) => predicate(x);
            return this;
        }

        /// <summary>
        /// Maps Y Value
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SeriesConfiguration<T> X(Func<T, int, double> predicate)
        {
            XValueMapper = predicate;
            return this;
        }

        /// <summary>
        /// Maps Y Value
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SeriesConfiguration<T> Y(Func<T, double> predicate)
        {
            YValueMapper = (x, i) => predicate(x);
            return this;
        }

        /// <summary>
        /// Max X Value
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SeriesConfiguration<T> Y(Func<T, int, double> predicate)
        {
            YValueMapper = predicate;
            return this;
        }

        /// <summary>
        /// Maps X Labels
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SeriesConfiguration<T> XFormat(Func<double, string> predicate)
        {
            XLabelFormatter = predicate;
            return this;
        }

        /// <summary>
        /// Maps Y labels
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SeriesConfiguration<T> YFormat(Func<double, string> predicate)
        {
            YLabelFormatter = predicate;
            return this;
        }

        public SeriesConfiguration<T> HasOptimization(Func<IEnumerable<T>, IEnumerable<Point>> predicate)
        {
            OptimizationMethod = predicate;
            return this;
        }
    }
}
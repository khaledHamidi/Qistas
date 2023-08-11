namespace Qistas.QAnalyzer
{
    public struct QLinksPattern
    {

        /// <summary>
        /// Create a list of QPattern 
        /// </summary>
        /// <param name="qPattern">Qpatters spildded by comma ','</param>
        /// <returns></returns>
        public static List<QLinksPattern> Create(params QLinksPattern[] qPattern)
        {
            List<QLinksPattern> x = new List<QLinksPattern>();
            x.AddRange(qPattern);
            return x;
        }


        public static bool TryParse(ref string data, QLinksPattern pattern)
        {
            switch (pattern.QProfileType)
            {
                case QPatternType.Equal:
                    {
                        if (data.Contains(pattern.Equals)) { data = pattern.Equals; return true; }
                        break;
                    }

                case QPatternType.Ends:
                    {
                        if (data.Contains(pattern.Ends))
                        {
                            data = data.Substring(0, data.IndexOf(pattern.Ends));
                            return true;
                        }
                        break;
                    }

                case QPatternType.between:
                    {
                        if (data.Contains(pattern.Begins))
                        {
                            var tempData = data.Substring(data.IndexOf(pattern.Begins) + pattern.Begins.Length);

                            if (tempData.Contains(pattern.Ends))
                            {

                                data = tempData.Substring(0, tempData.LastIndexOf(pattern.Ends));
                                return true;
                            }
                            break;
                        }
                        break;
                    }
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">string data to check if any pattern can be success.</param>
        /// <param name="qPattrens">qPattrens[0] is the pattern if success</param>
        /// <returns></returns>
        public static int TryParse(ref string data, List<QLinksPattern> qPattrens)
        {
            foreach (var pattern in qPattrens)
            {
                if (TryParse(ref data, pattern))
                {
                    return pattern.Id;
                }
            }
            return -1;
        }

        public int Id { get; set; }
        public string Begins { get; set; }
        public string Ends { get; set; }
        public new string Equals { get; set; }







        public QPatternType QProfileType;
        public static QLinksPattern EqualTo(string Equals, int id = -1)
        {
            return new QLinksPattern { Id = id, Equals = Equals, QProfileType = QPatternType.Equal };

        }
        public static QLinksPattern BeginsWith(string Begins, int id = -1)
        {
            return new QLinksPattern { Id = id, Begins = Begins, QProfileType = QPatternType.Begins };

        }
        public static QLinksPattern EndsWith(string Ends, int id = -1)
        {
            return new QLinksPattern { Id = id, Ends = Ends, QProfileType = QPatternType.Ends };

        }
        public static QLinksPattern Between(string Begins, string Ends, int id = -1)
        {
            return new QLinksPattern { Id = id, Begins = Begins, Ends = Ends, QProfileType = QPatternType.between };
        }
    }



    public enum QPatternType
    {
        Equal,
        Ends,
        between,
        Begins,
    }
}

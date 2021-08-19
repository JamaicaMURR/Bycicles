using Bycicles.StringExtensions;
using System;
using System.Collections.Generic;

namespace Bycicles
{
    public class EasyGrid
    {
        public const string SHORTENING_MARKER_DEFAULT = "~";

        //=======================================================================================================================================================================
        List<string> _lines, _cuts;
        List<Field> _fields;

        ScoreBoard<Field, int> _sortedFields;

        Func<string> DoOnGetGrid;

        //=======================================================================================================================================================================
        public string ShorteningMarker { get; set; } = SHORTENING_MARKER_DEFAULT;

        //=======================================================================================================================================================================
        public EasyGrid()
        {
            _lines = new List<string>();
            _fields = new List<Field>();

            DoOnGetGrid = delegate ()
            {
                Assemble();
                return DoOnGetGrid();
            };
        }

        //=======================================================================================================================================================================
        public void AddLine(string line = "") => _lines.Add(line);

        //=======================================================================================================================================================================
        public void LetField(string code, Func<object> contentGetter) => _fields.Add(new Field(code, contentGetter, ShorteningMarker));

        //=======================================================================================================================================================================
        public string GetGrid() => DoOnGetGrid();

        //=======================================================================================================================================================================
        public void Assemble()
        {
            string fullText = "";

            foreach(string line in _lines)
                fullText += line + "\n";

            _cuts = new();

            if(_fields.Count > 0)
            {
                _sortedFields = GetSortedFields(fullText);

                for(int i = 0; i < _sortedFields.Count; i++)
                    _cuts.Add(fullText.Remove(_sortedFields[i].Item2 + _sortedFields[i].Item1.Length));

                for(int i = _cuts.Count - 1; i > 0; i--)
                {
                    _cuts[i] = _cuts[i].Remove(_sortedFields[i].Item2);
                    _cuts[i] = _cuts[i].Remove(0, _cuts[i - 1].Length);
                }

                _cuts[0] = _cuts[0].Remove(_sortedFields[0].Item2);
                _cuts.Add(fullText.Remove(0, _sortedFields[_sortedFields.Count - 1].Item2 + _sortedFields[_sortedFields.Count - 1].Item1.Length));
            }
            else
                _cuts.Add(fullText);

            DoOnGetGrid = ActualGetGrid;
        }

        //=======================================================================================================================================================================
        ScoreBoard<Field, int> GetSortedFields(string text)
        {
            ScoreBoard<Field, int> result = new ScoreBoard<Field, int>(_fields.Count, ScoreBoardMode.LowerBest);

            foreach(Field f in _fields)
                result.TryToInsert(f, text.IndexOf(f.Code));

            return result;
        }

        //=======================================================================================================================================================================
        public string ActualGetGrid()
        {
            string result = "";

            if(_sortedFields != null)
            {
                for(int i = 0; i < _sortedFields.Count; i++)
                {
                    result += _cuts[i];
                    result += _sortedFields[i].Item1.GetContent();
                }

                result += _cuts[_cuts.Count - 1];
            }
            else
                result += _cuts[0];

            return result;
        }

        //=======================================================================================================================================================================
        class Field
        {
            readonly string _shorteningMarker;

            public string Code { get; }

            public int Length => Code.Length;

            public Func<string> GetContent { get; }

            public Field(string code, Func<object> contentGetter, string shorteningMarker)
            {
                Code = code;

                _shorteningMarker = shorteningMarker;

                if(contentGetter is Func<string>)
                    GetContent = () => (contentGetter as Func<string>)().FormToLengthRight(Length, _shorteningMarker);
                else
                    GetContent = () => contentGetter().ToString().FormToLengthRight(Length, _shorteningMarker);

            }
        }
    }
}

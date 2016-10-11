using System;
using System.IO;
using Baseline;

namespace StoryTeller.Model.Persistence.Markdown
{
    public class CommentMode : IReaderMode
    {
        private readonly Comment _comment = new Comment();


        public CommentMode(Section section)
        {
            section.Children.Add(_comment);
        }

        public CommentMode(Specification spec)
        {
            spec.Children.Add(_comment);
        }

        public IReaderMode Read(int indention, string text)
        {
            if (text.StartsSectionOrStep())
            {
                return null;
            }

            if (_comment.Text.IsEmpty())
            {
                _comment.Text = text;
            }
            else
            {
                _comment.Text += Environment.NewLine + text;
            }

            return this;
        }

        public int Indention { get; set; }
    }
}
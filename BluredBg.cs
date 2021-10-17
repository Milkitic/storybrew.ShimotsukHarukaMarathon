using Coosu.Storyboard;
using Coosu.Storyboard.Storybrew.Text;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class BluredBg : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = new Layer();

        }

        private List<BookmarkObj> GetBookmarkObj()
        {
            var actualBookmarks = Beatmap.Bookmarks;
            var bookmarks = new List<BookmarkObj>();
            int i = 0;
            int tmpLead = 0;
            int tmpFadeIn = 0;
            int tmpFadeOut = 0;
            foreach (var bm in actualBookmarks)
            {
                if (i == 0) tmpLead = bm;
                else if (i == 1) tmpFadeIn = bm;
                else if (i == 2)
                {
                    tmpFadeOut = bm;
                    i = 0;
                    var bmObj = new BookmarkObj(tmpLead, tmpFadeIn, tmpFadeOut);
                    bookmarks.Add(bmObj);
                    tmpLead = 0;
                    tmpFadeIn = 0;
                    tmpFadeOut = 0;
                    continue;
                }

                i++;
            }

            return bookmarks;
        }
    }

    public struct BookmarkObj
    {
        public BookmarkObj(int lead, int fadeIn, int fadeOut)
        {
            Lead = lead;
            FadeIn = fadeIn;
            FadeOut = fadeOut;
        }

        public int Lead { get; set; }
        public int FadeIn { get; set; }
        public int FadeOut { get; set; }
    }
}

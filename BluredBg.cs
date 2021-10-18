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
            var bookmarks = GetBookmarkObj();
            for (int i = 0; i < bookmarks.Count; i++)
            {
                BookmarkObj item = bookmarks[i];
                var fileName = (i + 1) + "_b.png";
                var bgBehind = layer.CreateSprite(@"SB\blurs\" + fileName);
                var time = Math.Min(3000, item.FadeIn - item.Lead);
                bgBehind.Fade(2, item.Lead - 1000, item.Lead + time, 0, 1);
                bgBehind.Fade(item.Lead + 3000, item.FadeOut, 1);
                bgBehind.Fade(2, item.FadeOut, item.FadeOut + 3000, 1, 0);
                var s = 0.4447916666666667;
                bgBehind.Scale(0, item.Lead - 1000, item.FadeOut + 3000, s, s * 1.5);
                bgBehind.Rotate(0, item.Lead - 1000, item.FadeOut + 3000, 0, 0.3);

                var gradient = layer.CreateSprite(@"SB\components\gradient.png");
                gradient.Fade(2, item.Lead - 1000, item.Lead + time, 0, 0.04);
                gradient.Move(item.Lead - 1000, item.FadeOut + 3000, -450, 240, 640 + 450, 240);
                gradient.Fade(2, item.FadeOut, item.FadeOut + 3000, 0.04, 0);
                gradient.Vector(item.Lead, 5, 3);
                gradient.Additive(item.Lead);
                var timeHaha = item.Lead - 1000d;
                while (timeHaha < item.FadeOut + 3000)
                {
                    var elapsed = Random(3000d, 5000d);
                    var filter = layer.CreateSprite(@"SB\components\filter.jpg");
                    filter.Rotate(timeHaha, timeHaha + elapsed, Random(-3.14, 0), Random(0, 3.14));
                    filter.Fade(timeHaha, timeHaha + 500, 0, 0.04);
                    // filter.Fade(timeHaha, 0.02);
                    filter.Fade(timeHaha + elapsed - 500, timeHaha + elapsed, 0.04, 0);
                    filter.Vector(timeHaha, 1.3, 2.2);
                    filter.Additive(timeHaha);
                    filter.Move(timeHaha, 320 + Random(-75, 75), 240 + Random(-75, 75));
                    timeHaha += elapsed / 2;
                }
            }

            for (int i = 0; i < bookmarks.Count; i++)
            {
                BookmarkObj item = bookmarks[i];
                var fileName = (i + 1) + "_b.png";
                fileName = (i + 1) + ".jpg";
                var bg = layer.CreateSprite(@"SB\ppts\" + fileName);
                var time = Math.Min(3000, item.FadeIn - item.Lead);
                bg.Fade(2, item.Lead - 1000, item.Lead + time, 0, 1);
                bg.Fade(item.Lead + 3000, item.FadeOut, 1);
                bg.Fade(2, item.FadeOut, item.FadeOut + 3000, 1, 0);
                bg.Scale(item.Lead, 0.546);
                bg.Move(item.Lead, 196, 232);
            }

            var frame = layer.CreateSprite(@"SB\components\frame.png");
            frame.Fade(2, -1000, 0, 0, 1);
            frame.Fade(0, 1294775, 1);
            frame.Fade(2, 1294775, 1294775 + 3000, 1, 0);
            frame.Scale(0, 0.440);
            frame.Move(0, 195, 231);
            frame.Color(0, 37, 33, 32);
            layer.ExecuteBrew(this);
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

        private struct BookmarkObj
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

}

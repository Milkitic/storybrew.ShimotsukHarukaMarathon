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
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StorybrewScripts
{
    public class Meta : StoryboardObjectGenerator
    {
        private const int x = 600;
        public override void Generate()
        {
            var layer = new Layer();
            var secTitle = layer.CreateText("~ Title ~", 0, x, 50, ConfigSection());
            FixSection(secTitle);
            var secSource = layer.CreateText("~ Source ~", 0, x, 155, ConfigSection());
            FixSection(secSource);
            var secBpm = layer.CreateText("~ BPM ~", 0, x, 260, ConfigSection());
            FixSection(secBpm);
            var secBpmMin = layer.CreateText("MIN", 0, x - 100, 260, ConfigSubSection());
            FixSection(secBpmMin);
            var secBpmMax = layer.CreateText("MAX", 0, x + 100, 260, ConfigSubSection());
            FixSection(secBpmMax);
            var secMapper = layer.CreateText("~ Mapper ~", 0, x, 365, ConfigSection());
            FixSection(secMapper);

            List<BookmarkObj> bookmarks = GetBookmarkObj();

            // Log(JsonConvert.SerializeObject(bookmarks, Formatting.Indented));
            SetValue(layer, bookmarks);
        }

        private void SetValue(Layer layer, List<BookmarkObj> bookmarks)
        {
            var text = File.ReadAllText(Path.Combine(ProjectPath, "info.json"));
            JArray jArr = Newtonsoft.Json.Linq.JArray.Parse(text);

            var titleStrs = new List<string>();
            var sourceStrs = new List<string>();
            var mapperStrs = new List<string>();

            foreach (var jToken in jArr)
            {
                var jObj = (JObject)jToken;
                var titleStr = jObj["title"].Value<string>();
                var sourceStr = jObj["source"].Value<string>();
                var mapperStr = jObj["mapper"].Value<string>();

                titleStrs.Add(titleStr);
                sourceStrs.Add(sourceStr);
                mapperStrs.Add(mapperStr);
            }

            for (int i = 0; i < bookmarks.Count; i++)
            {
                var bmObj = bookmarks[i];
                var titleStr = titleStrs[i];
                var sourceStr = sourceStrs[i];
                var mapperStr = mapperStrs[i];

                var title = layer.CreateText(titleStr, bmObj.FadeIn, x, 95, k =>
                {
                    GeneralConfigContent(k);
                    k.WithWordGap(-2);
                    if (i == 4)
                    {
                        k.ScaleXBy(0.48);
                        k.ScaleYBy(0.65);
                    }
                    else if (i == 6)
                    {
                        k.ScaleXBy(0.48);
                        k.ScaleYBy(0.65);
                    }
                });
                FixContentSection(title, bmObj.FadeIn, bmObj.FadeOut);

                var source = layer.CreateText(sourceStr, bmObj.FadeIn, x, 200, k =>
                {
                    GeneralConfigContent(k);
                    k.WithWordGap(-3);
                    if (i == 0)
                    {
                        k.ScaleXBy(0.55);
                        k.ScaleYBy(0.65);
                    }
                    else if (i == 1)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 2)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 3)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 4)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 5)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 6)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 7)
                    {
                        k.ScaleXBy(0.48);
                        k.ScaleYBy(0.65);
                    }
                    else if (i == 8)
                    {
                        k.ScaleXBy(0.48);
                        k.ScaleYBy(0.65);
                    }
                    else if (i == 9)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 10)
                    {
                        k.ScaleXBy(0.7);
                        k.ScaleYBy(0.7);
                    }
                    else if (i == 11)
                    {
                        k.ScaleXBy(0.58);
                        k.ScaleYBy(0.65);
                    }
                    else if (i == 12)
                    {
                        k.ScaleXBy(0.48);
                        k.ScaleYBy(0.65);
                    }
                    else if (i == 13)
                    {
                        k.ScaleXBy(0.45);
                        k.ScaleYBy(0.58);
                    }
                });
                FixContentSection(source, bmObj.FadeIn, bmObj.FadeOut);

                var timings = Beatmap.TimingPoints
                    .Where(k => !k.IsInherited &&
                        k.Offset >= bmObj.FadeIn &&
                        k.Offset < bmObj.FadeOut)
                    .ToList();
                if (timings.Count == 0)
                    timings.Add(Beatmap.TimingPoints
                        .LastOrDefault(k => !k.IsInherited && k.Offset < bmObj.FadeIn));
                Log(timings.Count);
                var minBpm = timings.Min(k => k.Bpm);
                var maxBpm = timings.Max(k => k.Bpm);
                for (int j = 0; j < timings.Count; j++)
                {
                    ControlPoint timing = timings[j];
                    var showTime = timing.Offset;
                    var hideTime = j == timings.Count - 1 ? bmObj.FadeOut : timings[j + 1].Offset;
                    var bpm = layer.CreateText(timing.Bpm.ToString("0.###"), timing.Offset, x, 305, k =>
                     {
                         GeneralConfigContent2(k);
                     });
                    foreach (var item in bpm)
                    {
                        if (j == 0)
                            item.Fade(2, showTime - 200, showTime, 0, 1);
                        else
                            item.Fade(2, showTime - 50, showTime, 0, 1);
                        item.Fade(showTime, hideTime, 1);
                        if (j == timings.Count - 1)
                            item.Fade(0, hideTime, hideTime + 3000, 1, 0);
                        else
                            item.Fade(1, hideTime, hideTime + 50, 1, 0);
                    }
                }
                var bpm2 = layer.CreateText(minBpm.ToString("0.###"), bmObj.FadeIn, x - 102, 305, k =>
                 {
                     GeneralConfigContent2(k);
                     k.ScaleXBy(0.5);
                     k.ScaleYBy(0.5);
                 });
                FixContentSection(bpm2, bmObj.FadeIn, bmObj.FadeOut);
                var bpm3 = layer.CreateText(maxBpm.ToString("0.###"), bmObj.FadeIn, x + 98, 305, k =>
                  {
                      GeneralConfigContent2(k);
                      k.ScaleXBy(0.5);
                      k.ScaleYBy(0.5);
                  });
                FixContentSection(bpm3, bmObj.FadeIn, bmObj.FadeOut);

                var mapper = layer.CreateText(mapperStr, bmObj.FadeIn, x, 410, k =>
                {
                    GeneralConfigContent2(k);
                    k.WithWordGap(-3);
                    k.ScaleXBy(0.8);
                });
                FixContentSection(mapper, bmObj.FadeIn, bmObj.FadeOut);

            }

            // var title = layer.CreateText("白夜幻想譚", 0, x, 95, k =>
            // {
            //     GeneralConfigContent(k);
            // });
            // FixSection(title);

            // var source = layer.CreateText("イリスのアトリエ エターナルマナ", 0, x, 200, k =>
            // {
            //     GeneralConfigContent(k);
            //     k.WithWordGap(-3);
            //     k.ScaleXBy(0.6);
            // });
            // FixSection(source);

            // var bpm = layer.CreateText("128", 0, x, 305, k =>
            // {
            //     GeneralConfigContent2(k);
            // });
            // FixSection(bpm);

            // var mapper = layer.CreateText("Gust", 0, x, 410, k =>
            // {
            //     GeneralConfigContent2(k);
            // });
            // FixSection(mapper);


            layer.ExecuteBrew(this);
        }

        private void FixContentSection(SpriteGroup group, int fadeIn, int fadeOut)
        {
            foreach (var item in group)
            {
                item.Fade(2, fadeIn - 200, fadeIn, 0, 1);
                item.Fade(fadeIn, fadeOut, 1);
                item.Fade(0, fadeOut, fadeOut + 3000, 1, 0);
            }
        }

        private static void FixSection(SpriteGroup group)
        {
            foreach (var item in group)
            {
                item.Fade(2, 0 - 1000, 0, 0, 1);
                item.Fade(0, 1294775, 1);
                item.Fade(0, 1294775, 1294775 + 1500, 1, 0);
            }
        }

        private static void GeneralConfigContent2(CoosuTextOptionsBuilder k)
        {
            k.WithIdentifier("content2");
            // k.WithFontFamily("SWSimp");
            k.WithFontFamily("BIZ UDMincho");
            k.ScaleXBy(0.7);
            k.ScaleYBy(0.7);
            k.WithFontSize(32);
            k.WithShadow("#A0FFFFFF", 12, depth: 0);
        }

        private static void GeneralConfigContent(CoosuTextOptionsBuilder k)
        {
            k.WithIdentifier("content");
            k.WithFontFamily("BIZ UDMincho");
            k.ScaleXBy(0.7);
            k.ScaleYBy(0.7);
            k.WithFontSize(30);
            k.WithShadow("#A0FFFFFF", 12, depth: 0);
        }

        private static Action<CoosuTextOptionsBuilder> ConfigSubSection()
        {
            return k =>
            {
                k.WithIdentifier("subsection");
                k.WithFontFamily("SWSimp");
                k.ScaleXBy(0.7);
                k.ScaleYBy(0.7);
                k.WithFontSize(22);
                k.WithShadow("#A0FFFFFF", 12, depth: 0);
            };
        }

        private static Action<CoosuTextOptionsBuilder> ConfigSection()
        {
            return k =>
            {
                k.WithIdentifier("section");
                k.WithFontFamily("SWSimp");
                k.ScaleXBy(0.7);
                k.ScaleYBy(0.7);
                k.WithFontSize(34);
                k.WithShadow("#A0FFFFFF", 12, depth: 0);
            };
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

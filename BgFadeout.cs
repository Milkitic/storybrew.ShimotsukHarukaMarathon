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
    public class BgFadeout : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("A");
            var g = layer.CreateSprite("BGP (3).jpg", OsbOrigin.Centre);
            g.Fade(0, 0);
        }
    }
}

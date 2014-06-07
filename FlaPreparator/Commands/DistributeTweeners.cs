using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlaPreparator.Mapping;

namespace FlaPreparator.Commands {
    class DistributeTweeners : ICommand {
        void ICommand.Run(Library lib) {
            foreach (var symbol in lib.Symbols) {
                foreach (var timeline in symbol.timeline) {
                    foreach (var layer in timeline.layers.FindAll((l) => { return l.HasTweens && ! "guide" . Equals(l.layerType); })) {
                        distributeTweners(layer);
                    }
                }
            }
        }

        void distributeTweners(DOMLayer layer) {
            int index = -1;
            while((index = layer.frames.FindIndex((f) => { return f.tweenType != null; })) != -1) {
                distributeTween(layer, index);
            }
        }

        void distributeTween(DOMLayer layer, int startIndex) {
            var startFrame = layer.frames[startIndex];
            var endFrame = layer.frames[startIndex + 1];

            startFrame.tweenType = null;
            startFrame.motionTweenSnap = null;
            startFrame.duration = "1";

            Point point = getTweenFramesOffset(startFrame, endFrame);
            int tweenDuration = Int32.Parse(endFrame.index) - Int32.Parse(startFrame.index) - 1;
            if(tweenDuration != 0) {
                var tx = double.Parse(point.x) / (tweenDuration + 1);
                var ty = double.Parse(point.y) / (tweenDuration + 1);
                var currentFrame = startFrame;
                while (tweenDuration != 0) {
                    currentFrame = currentFrame.Clone();
                    layer.frames.Insert(++startIndex, currentFrame);

                    currentFrame.tweenType = null;
                    currentFrame.motionTweenSnap = null;
                    currentFrame.duration = "1";

                    var symbol = (currentFrame.elements[0] as DOMSymbolInstance);
                    var symbol_offset = new Point();
                    symbol_offset.x = symbol.matrix[0].tx; symbol_offset.y = symbol.matrix[0].ty;

                    fixPoint(symbol_offset);

                    symbol.matrix[0].tx = (double.Parse(symbol_offset.x) + tx).ToString();
                    symbol.matrix[0].ty = (double.Parse(symbol_offset.y) + ty).ToString();
                    currentFrame.index = startIndex.ToString();

                    tweenDuration--;
                }
            }

        }

        void fixPoint(Point point) {
            point.x = point.x == null ? "0" : point.x;
            point.y = point.y == null ? "0" : point.y;
        }

        Point getTweenFramesOffset(DOMFrame startFrame, DOMFrame endFrame) {
            var startSymbol = (startFrame.elements[0] as DOMSymbolInstance);
            var endSymbol = (endFrame.elements[0] as DOMSymbolInstance);

            var start = new Point(); var end = new Point();
            if (startSymbol.matrix.Count != 0) { start.x = startSymbol.matrix[0].tx; start.y = startSymbol.matrix[0].ty; }
            if (endSymbol.matrix.Count != 0) { end.x = endSymbol.matrix[0].tx; end.y = endSymbol.matrix[0].ty; }

            fixPoint(start); fixPoint(end);

            var result = new Point();
            result.x = (double.Parse(end.x) - double.Parse(start.x)).ToString();
            result.y = (double.Parse(end.y) - double.Parse(start.y)).ToString();

            return result;
        }
    }
}

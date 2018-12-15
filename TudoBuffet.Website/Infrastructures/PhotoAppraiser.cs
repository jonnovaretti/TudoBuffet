using System;

namespace TudoBuffet.Website.Infrastructures
{
    public class PhotoAppraiser
    {
        private readonly int maxWidth;
        private readonly int maxHeight;
        private readonly int minimumWidth;
        private readonly int minimumHeight;

        public PhotoAppraiser(int maxWidth, int maxHeight, int minimumWidth, int minimumHeight)
        {
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            this.minimumWidth = minimumWidth;
            this.minimumHeight = minimumHeight;
        }

        public AppraisalResult AppraiseSize(int width, int height)
        {
            if (width < minimumWidth || height < minimumHeight)
                return AppraisalResult.InapropriatedSize;

            if (IsARectanglePortrait(width, height))
            {
                if (width > maxWidth || height > maxWidth)
                    return AppraisalResult.ShouldBeShortenIgnoringRatio;
            }
            else
            {
                if (height > maxHeight)
                    return AppraisalResult.ShouldBeShortenKeepingRatio;
            }

            return AppraisalResult.Keep;
        }

        private static bool IsARectanglePortrait(int width, int height)
        {
            return width >= height;
        }
    }

    public enum AppraisalResult
    {
        ShouldBeShortenKeepingRatio,
        ShouldBeShortenIgnoringRatio,
        Keep,
        InapropriatedSize
    }
}
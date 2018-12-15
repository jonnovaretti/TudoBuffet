using Microsoft.VisualStudio.TestTools.UnitTesting;
using TudoBuffet.Website.Infrastructures;

namespace TudoBuffet.Tests
{
    [TestClass]
    public class ResizePhotosTest
    {
        private const int MAX_WIDTH = 650;
        private const int MAX_HEIGHT = 400;
        private const int MINIMUM_WIDTH = 180;
        private const int MINIMUM_HEIGHT = 180;

        [TestMethod]
        public void VerifyIfReturnIsShouldBeShortenIgnoringRatioWhenPhotoIsBiggerThanMaximumAndItIsARectanglePortrait()
        {
            PhotoAppraiser photoAppraiser;
            AppraisalResult appraisalResult;

            photoAppraiser = new PhotoAppraiser(MAX_WIDTH, MAX_HEIGHT, MINIMUM_WIDTH, MINIMUM_HEIGHT);
            appraisalResult = photoAppraiser.AppraiseSize(700, 400);

            Assert.AreEqual(appraisalResult, AppraisalResult.ShouldBeShortenIgnoringRatio);
        }

        [TestMethod]
        public void VerifyIfReturnIsShouldBeShortenKeepingRatioWhenPhotoIsBiggerThanMaximumAndItIsARectanglePaisage()
        {
            PhotoAppraiser photoAppraiser;
            AppraisalResult appraisalResult;

            photoAppraiser = new PhotoAppraiser(MAX_WIDTH, MAX_HEIGHT, MINIMUM_WIDTH, MINIMUM_HEIGHT);
            appraisalResult = photoAppraiser.AppraiseSize(350, 700);

            Assert.AreEqual(appraisalResult, AppraisalResult.ShouldBeShortenKeepingRatio);
        }

        [TestMethod]
        public void VerifyIfReturnIsKeepWhenPhotoRespectLimits()
        {
            PhotoAppraiser photoAppraiser;
            AppraisalResult appraisalResult;

            photoAppraiser = new PhotoAppraiser(MAX_WIDTH, MAX_HEIGHT, MINIMUM_WIDTH, MINIMUM_HEIGHT);
            appraisalResult = photoAppraiser.AppraiseSize(600, 350);

            Assert.AreEqual(appraisalResult, AppraisalResult.Keep);
        }

        [TestMethod]
        public void VerifyIfReturnIsInaproppriatedWhenPhotoIsLessThanMinimum()
        {
            PhotoAppraiser photoAppraiser;
            AppraisalResult appraisalResult;

            photoAppraiser = new PhotoAppraiser(MAX_WIDTH, MAX_HEIGHT, MINIMUM_WIDTH, MINIMUM_HEIGHT);
            appraisalResult = photoAppraiser.AppraiseSize(150, 150);

            Assert.AreEqual(appraisalResult, AppraisalResult.InapropriatedSize);
        }
    }
}

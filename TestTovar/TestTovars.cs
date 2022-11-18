using FluentAssertions;
using UserStory_Men_Cher.Models;
using Xunit;

namespace TestTovar
{
    public class TestTovars
    {
        public SkladLib.TovarLib Tovars = new SkladLib.TovarLib();

        public void FillItem(ref Tovar tvr)
        {
            tvr.TovarName = "43D21D";
            tvr.Material = Material.Med;
            tvr.Size = "21x23x11";
            tvr.Count = 2301;
            tvr.MinCount = 342;
            tvr.Price = 2;
            tvr.AllPrice = tvr.Price * tvr.Count;
        }

        public void FillItemAnotherValue(ref Tovar tvr)
        {
            tvr.TovarName = "TR21A2";
            tvr.Material = Material.Hrom;
            tvr.Size = "11x13x09";
            tvr.Count = 1232;
            tvr.MinCount = 123;
            tvr.Price = 3;
            tvr.AllPrice = tvr.Price * tvr.Count;
        }

        [Fact]
        public void AddItem()
        {
            var tvr = new Tovar();
            FillItem(ref tvr);

            var checkAdd = Tovars.AddTovar(tvr);
            var checkAll = Tovars.GetTovar();

            checkAdd.Should().Be(tvr);
            checkAll.Should().HaveCount(1).And.NotBeEmpty();
        }

        [Fact]
        public void EditItem()
        {
            var oldtvr = new Tovar();
            FillItem(ref oldtvr);
            var newtvr = new Tovar();
            FillItemAnotherValue(ref newtvr);

            Tovars.AddTovar(oldtvr);

            Tovars.ChangeTovar(oldtvr, newtvr);

            var checkAll = Tovars.GetTovar();

            checkAll.Should().HaveCount(1).And.NotBeEmpty();
            Assert.Equal(checkAll[0], newtvr);
        }

        [Fact]
        public void DeleteItem()
        {
            var tvr = new Tovar();
            FillItem(ref tvr);

            Tovars.AddTovar(tvr);
            Tovars.RemoveTovar(tvr);

            var checkAll = Tovars.GetTovar();

            checkAll.Should().BeEmpty();
        }

        [Fact]
        public void PassedItems()
        {
            var firsttvr = new Tovar();
            FillItem(ref firsttvr);

            var secondtvr = new Tovar();
            FillItemAnotherValue(ref secondtvr);

            Tovars.AddTovar(firsttvr);
            Tovars.AddTovar(secondtvr);

            var checkAll = Tovars.GetTovar();

            checkAll.Should().HaveCount(2).And.NotBeEmpty();

            var checkPassed = Tovars.GetCountTovar;

            checkPassed.Should().NotBeNull();
        }

    }
}
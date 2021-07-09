﻿using DesignPatterns.Builders;
using FluentAssertions;
using Xunit;

namespace DesignPatterns.Tests.Builders
{
    public class ShoppingListSectionBuilderTests
    {
        [Fact]
        public void CreatingInstanceOfShoppingListSectionBuilder_Success()
        {
            using var sectionBuilder = new ShoppingListSectionBuilder();

            sectionBuilder.Should().NotBeNull();
        }

        [Fact]
        public void EmptyShoppingListSection_Success()
        {
            using var sectionBuilder = new ShoppingListSectionBuilder();

            var section = sectionBuilder.Build();

            section.Should().NotBeNull();
            section.Description.Should().BeNullOrEmpty();
            section.Items.Should().BeNullOrEmpty();
        }

        [Fact]
        public void AddingDescriptionToShoppingListSection_Success()
        {
            using var sectionBuilder = new ShoppingListSectionBuilder();

            var section = sectionBuilder.WithDescription("Food").Build();

            Assert.Equal("Food", section.Description);
        }

        [Fact]
        public void AddingMultipleItemsToSection_Success()
        {
            using var sectionBuilder = new ShoppingListSectionBuilder();

            var section = sectionBuilder.WithDescription("Snacks")
                .AppendItem("Popcorn", 1)
                .AppendItem("Cheetos", 2)
                .AppendItem("Cookies pack", 4)
                .Build();

            section.Items.Should()
                .NotBeNullOrEmpty()
                .And
                .HaveCount(3);
        }

        [Fact]
        public void AddingSameItemTwice_QuantityIsSummed_Success()
        {
            using var sectionBuilder = new ShoppingListSectionBuilder();

            var section = sectionBuilder.WithDescription("Snacks")
                .AppendItem("Popcorn", 1)
                .AppendItem("Popcorn", 6)
                .AppendItem("Cookies pack", 4)
                .Build();
            
            section.Items.Should()
                .HaveCount(2)
                .And
                .ContainSingle(item => item.Name == "Popcorn" && item.Quantity == 7);
        }
    }
}
using System.ComponentModel.DataAnnotations;
using RackPeek.Domain.Helpers;

namespace Tests.Unit.Helpers;

public class ThrowIfInvalidLabelTests
{
    [Fact]
    public void label_key__valid_key__does_not_throw()
    {
        var ex = Record.Exception(() => ThrowIfInvalid.LabelKey("env"));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void label_key__empty_or_whitespace__throws_validation_exception(string key)
    {
        Assert.Throws<ValidationException>(() => ThrowIfInvalid.LabelKey(key));
    }

    [Fact]
    public void label_key__exceeds_50_chars__throws_validation_exception()
    {
        var longKey = new string('k', 51);
        var ex = Assert.Throws<ValidationException>(() => ThrowIfInvalid.LabelKey(longKey));
        Assert.Contains("too long", ex.Message);
    }

    [Fact]
    public void label_key__exactly_50_chars__does_not_throw()
    {
        var key = new string('k', 50);
        var ex = Record.Exception(() => ThrowIfInvalid.LabelKey(key));
        Assert.Null(ex);
    }

    [Fact]
    public void label_value__valid_value__does_not_throw()
    {
        var ex = Record.Exception(() => ThrowIfInvalid.LabelValue("production"));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void label_value__empty_or_whitespace__throws_validation_exception(string value)
    {
        Assert.Throws<ValidationException>(() => ThrowIfInvalid.LabelValue(value));
    }

    [Fact]
    public void label_value__exceeds_200_chars__throws_validation_exception()
    {
        var longValue = new string('v', 201);
        var ex = Assert.Throws<ValidationException>(() => ThrowIfInvalid.LabelValue(longValue));
        Assert.Contains("too long", ex.Message);
    }

    [Fact]
    public void label_value__exactly_200_chars__does_not_throw()
    {
        var value = new string('v', 200);
        var ex = Record.Exception(() => ThrowIfInvalid.LabelValue(value));
        Assert.Null(ex);
    }
}

using HtmlAgilityPack;

namespace CommunityToolkit.Labs.WinUI.MarkdigUI.TextElements.Html;

internal class MyInline : IAddChild
{
    private HtmlNode _htmlNode;
    private Paragraph _paragraph;
    private InlineUIContainer _inlineUIContainer;
    private RichTextBlock _richTextBlock;

    public TextElement TextElement
    {
        get => _inlineUIContainer;
    }

    public MyInline(HtmlNode inline)
    {
        _htmlNode = inline;
        _paragraph = new Paragraph();
        _inlineUIContainer = new InlineUIContainer();
        _richTextBlock = new RichTextBlock();
        _richTextBlock.Blocks.Add(_paragraph);

        _richTextBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
        _inlineUIContainer.Child = _richTextBlock;
    }

    public void AddChild(IAddChild child)
    {
        if (child.TextElement is Inline inlineChild)
        {
            _paragraph.Inlines.Add(inlineChild);
        }
        // we shouldn't support rendering block in inline
        // but if we want to support it, we can do it like this:
        //else if (child.TextElement is Block blockChild)
        //{
        //    _richTextBlock.Blocks.Add(blockChild);
        //    // if we add a new block to an inline container,
        //    // if the next child is inline, it needs to be added after the block
        //    // so we add a new paragraph. This way the next time
        //    // AddChild is called, it's added to the new paragraph
        //    _paragraph = new Paragraph();
        //    _richTextBlock.Blocks.Add(_paragraph);
        //}
    }
}

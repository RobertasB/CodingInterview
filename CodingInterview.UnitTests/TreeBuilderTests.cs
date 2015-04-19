using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingInterview.UnitTests
{
    [TestFixture]
    public class TreeBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            // Arrange
            _sut = new TreeBuilder<MusicGenre>();
        }

        [Test]
        public void ShouldReturnEmptyList()
        {
            // Act
            var tree = _sut.BuildTree();

            // Assert
            Assert.That(tree, Is.Empty);
        }

        [Test]
        public void ShouldReturnSingleNode()
        {
            // Act
            _sut.AddNode(1, new MusicGenre(1, "Classical"), null);
            var tree = _sut.BuildTree();

            // Assert
            var node = tree.Single();
            Assert.That(node.Parent, Is.Null);
            Assert.That(node.Value.Id, Is.EqualTo(1));
            Assert.That(node.Children, Is.Empty);
        }

        [Test]
        public void ShouldWorkWhenAddingChildrenPriorToParent()
        {
            // Act
            _sut.AddNode(11, new MusicGenre(11, "Insturmental"), 1);
            _sut.AddNode(12, new MusicGenre(12, "Vocal"), 1);
            _sut.AddNode(1, new MusicGenre(1, "Classical"), null);
            var tree = _sut.BuildTree();

            // Assert
            var node = tree.Single();
            Assert.That(node.Parent, Is.Null);
            Assert.That(node.Value.Id, Is.EqualTo(1));
            Assert.That(node.Children, Has.Count.EqualTo(2));
        }

        [Test]
        public void ShouldBuildFullTree()
        {
            // Act
            _sut.AddNodes(_items, i => i.Id, i => new MusicGenre(i.Id, i.Name), i => i.ParnetId);
            var tree = _sut.BuildTree();

            // Assert
            Assert.That(tree, Has.Count.EqualTo(3));
            var classical = tree.Single(n => n.Value.Id == 1);
            Assert.That(classical.Children, Has.Count.EqualTo(2));
            var vocal = classical.Children.Single(n => n.Value.Id == 12);
            Assert.That(vocal.Children, Has.Count.EqualTo(2));
            var opera = vocal.Children.Single(n => n.Value.Id == 121);
            Assert.That(opera, Is.Not.Null);
        }

        private TreeBuilder<MusicGenre> _sut;

        private readonly List<ListItem> _items = new List<ListItem>
        {
            // http://gritslab.gatech.edu/Pickem/wp-content/gallery/cs6601_ai_portfolio/music_genre_taxonomy.png

            // Classical
            new ListItem(1, "Classical", null),
            new ListItem(11, "Instrumental", 1),
            new ListItem(12, "Vocal", 1),
            new ListItem(111, "Piano", 11),
            new ListItem(112, "Orchestra", 11),
            new ListItem(121, "Opera", 12),
            new ListItem(122, "Chorus", 12),

            // Modern
            new ListItem(2, "Pop/Rock", null),
            new ListItem(21, "Organic", 2),
            new ListItem(22, "Electronic", 2),
            new ListItem(211, "Rock", 21),
            new ListItem(212, "Country", 21),
            new ListItem(213, "Pop", 22),
            new ListItem(214, "Techno", 22),
            new ListItem(2111, "Soft Rock", 211),
            new ListItem(2112, "Hard Rock", 211),
            new ListItem(2113, "Heavy Metal", 211),
            new ListItem(2121, "Soft Country", 212),
            new ListItem(2122, "Dancing Country", 212),


            // Dance
            new ListItem(3, "Dance", null),
            new ListItem(31, "Vocal", 3),
            new ListItem(32, "Percussion", 3),
            new ListItem(311, "Hip-Hop", 31),
            new ListItem(312, "Reggae", 31),
            new ListItem(321, "Jazz", 32),
            new ListItem(322, "Latin", 32)
        };
    }
}
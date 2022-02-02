using BlazorMartianRover.Components;
using Bunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MartianRoverTests
{
    
    public class BlazorTest
    {
        [Fact]
        public void La_console_de_commande_doit_exister()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<MartianRoverCommandPanel>();

            cut.Find("[data-martian-rover-command-panel]").Should().NotBeNull();
        }
        [Fact]
        public void L_utilisateur_peut_envoyer_une_commande_au_rover()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<MartianRoverCommandPanel>();

            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").Should().NotBeNull();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").TextContent
                .Should().Be("Envoyer");
        }
    }
}

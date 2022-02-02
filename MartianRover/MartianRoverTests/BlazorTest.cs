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
        public void L_utilisateur_ne_peut_pas_envoyer_une_commande_au_rover()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<MartianRoverCommandPanel>();

            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").Should().NotBeNull();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").TextContent.Should().Be("Envoyer");
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            var attr = bouton.Attributes;
            bouton.HasAttribute("disabled").Should().BeTrue();
        }

        [Fact]
        public void L_utilisateur_peut_saisir_une_commande()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<MartianRoverCommandPanel>();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]").Should().NotBeNull();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]").TextContent.Should().BeEmpty();
        }


        [Fact]
        public void Lorsque_l_utilisateur_saisi_une_commande_valide_alors_il_peut_lenvoyer_au_rover()
        {
            using var ctx = new TestContext();
            var cut = ctx.RenderComponent<MartianRoverCommandPanel>();
            
            
            var input = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]");
            input.SetAttribute("value", "f");
            
            cut.Instance.Command.Should().Be("f");
            //cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]").GetAttribute("value").Should().Be("f");
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            var attr = bouton.Attributes;
            bouton.HasAttribute("disabled").Should().BeFalse();
            
        }
    }
}

using BlazorMartianRover.Components;
using Bunit;
using FluentAssertions;
using System;
using Xunit;

namespace MartianRoverTests
{

    public class BlazorTest : IDisposable
    {
        readonly TestContext ctx;

        IRenderedComponent<MartianRoverCommandPanel> LePanneauDeCommande()
        {

            var component = ctx.RenderComponent<MartianRoverCommandPanel>();
            component.Instance.MarsRover = new MarsRover(new int[] { 1, 2 }, "N", new int[] { 10, 11 });
            return component;
        }
        IRenderedComponent<MartianRoverCommandPanel> GetComponentSansRover() => ctx.RenderComponent<MartianRoverCommandPanel>();

        public BlazorTest()
        {
            ctx = new TestContext();
        }

        [Fact]
        public void L_utilisateur_ne_peut_pas_envoyer_une_commande_au_rover()
        {
            var cut = LePanneauDeCommande();
            On_ne_peut_pas_envoyer_une_comande(cut);
        }

        private void On_saisie_la_commande(IRenderedComponent<MartianRoverCommandPanel> cut, string uneCommande)
        {
            var input = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]");
            input.Input(uneCommande);
        }

        private void On_ne_peut_pas_envoyer_une_comande(IRenderedComponent<MartianRoverCommandPanel> cut)
        {
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").Should().NotBeNull();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").TextContent.Should().Be("Envoyer");
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            bouton.HasAttribute("disabled").Should().BeTrue();
        }

        [Fact]
        public void L_utilisateur_peut_saisir_une_commande()
        {
            var cut = LePanneauDeCommande();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]").Should().NotBeNull();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]").TextContent.Should().BeEmpty();
        }


        [Fact]
        public void Lorsque_l_utilisateur_saisi_une_commande_valide_et_quun_rover_est_present_alors_il_peut_lui_envoyer()
        {
            var uneCommandeValide = "f";
            var cut = LePanneauDeCommande();

            On_saisie_la_commande(cut, uneCommandeValide);

            cut.Instance.Command.Should().Be(uneCommandeValide);
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            bouton.HasAttribute("disabled").Should().BeFalse();
        }


        [Theory]
        [InlineData("")]
        [InlineData("azef")]
        public void Lorsque_l_utilisateur_saisi_une_commande_invalide_alors_il_ne_peut_pas_lenvoyer_au_rover(string uneCommandeInvalide)
        {
            var cut = LePanneauDeCommande();

            On_saisie_la_commande(cut, uneCommandeInvalide);
            cut.Instance.Command.Should().Be(uneCommandeInvalide);
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            bouton.HasAttribute("disabled").Should().BeTrue();
        }

        [Fact]
        public void On_ne_peut_pas_envoyer_une_commande_sans_rover()
        {
            var uneCommandeValide = "f";
            var cut = GetComponentSansRover();
            On_saisie_la_commande(cut, uneCommandeValide);

            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");

            bouton.HasAttribute("disabled").Should().BeTrue();
        }

        [Fact]
        public void Lorsque_l_utilisateur_envoie_forward_au_rover_il_avance()
        {
            var uneCommandeValide = "f";
            var cut = LePanneauDeCommande();
            On_saisie_la_commande(cut, uneCommandeValide);

            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            bouton.Click();

            cut.Instance.MarsRover.Position.Should().BeEquivalentTo(new int[] { 1, 3 }, a => a.WithStrictOrdering());
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}

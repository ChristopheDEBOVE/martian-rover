using BlazorMartianRover.Components;
using Bunit;
using FluentAssertions;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MartianRoverTests
{
    public class BlazorTest : IDisposable
    {
        readonly TestContext ctx;
        IRenderedComponent<MartianRoverCommandPanel> GetComponent() => ctx.RenderComponent<MartianRoverCommandPanel>();
        private Mock<ICommandValidator> _mockCommandValidator = new Mock<ICommandValidator>(); 
        public BlazorTest()
        {
            _mockCommandValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(false);
            
            ctx = new TestContext();
            ctx.Services.AddSingleton<ICommandValidator>(_mockCommandValidator.Object);
 
        }
        
        [Fact]
        public void La_console_de_commande_doit_exister()
        {
            var cut = GetComponent();
            cut.Find("[data-martian-rover-command-panel]").Should().NotBeNull();
        }

        [Fact]
        public void L_utilisateur_ne_peut_pas_envoyer_une_commande_au_rover()
        {
            var cut = GetComponent();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").Should().NotBeNull();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]").TextContent.Should().Be("Envoyer");
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            var attr = bouton.Attributes;
            bouton.HasAttribute("disabled").Should().BeTrue();
        }

        [Fact]
        public void L_utilisateur_peut_saisir_une_commande()
        {
            var cut = GetComponent();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]").Should().NotBeNull();
            cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]").TextContent.Should().BeEmpty();
        }


        [Fact]
        public void Lorsque_l_utilisateur_saisi_une_commande_valide_alors_il_peut_lenvoyer_au_rover()
        { 
            var uneCommandeValide = "kldmskldm";
            _mockCommandValidator.Setup(x => x.Validate(uneCommandeValide)).Returns(true);
            
            var cut = GetComponent();

            var input =  cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]");
            input.Change(uneCommandeValide);
            cut.Instance.Command.Should().Be(uneCommandeValide);
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            bouton.HasAttribute("disabled").Should().BeFalse();
        }
        
        [Fact]
        public void Lorsque_l_utilisateur_saisi_une_commande_invalide_alors_il_ne_peut_pas_lenvoyer_au_rover()
        { 
            var uneCommandeInvalide = "kldmskldm";
            _mockCommandValidator.Setup(x => x.Validate(uneCommandeInvalide)).Returns(false);
            
            var cut = GetComponent();

            var input =  cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]");
            input.Change(uneCommandeInvalide);
            cut.Instance.Command.Should().Be(uneCommandeInvalide);
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            bouton.HasAttribute("disabled").Should().BeTrue();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}

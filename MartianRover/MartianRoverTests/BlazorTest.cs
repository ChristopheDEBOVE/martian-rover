using BlazorMartianRover.Components;
using Bunit;
using FluentAssertions;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRoverTests
{

    public class BlazorTest : IDisposable
    {
        readonly TestContext ctx;
        IRenderedComponent<MartianRoverCommandPanel> GetComponent() => ctx.RenderComponent<MartianRoverCommandPanel>();
        
        public BlazorTest()
        {
            ctx = new TestContext();
            ctx.Services.AddSingleton<ICommandValidator>(new CommandValidatorStub());
 
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
            ctx.Services.AddSingleton<ICommandValidator>(new CommandValidatorStub(true, uneCommandeValide));
            var cut = GetComponent();

            var input =  cut.Find("[data-martian-rover-command-panel] [data-martian-rover-command-input]");
            input.Change(uneCommandeValide);
            cut.Instance.Command.Should().Be(uneCommandeValide);
            var bouton = cut.Find("[data-martian-rover-command-panel] [data-martian-rover-send-command-action]");
            bouton.HasAttribute("disabled").Should().BeFalse();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
  
    class CommandValidatorStub: ICommandValidator{
        private readonly bool result;
        private readonly string expectedCommand;

        public CommandValidatorStub()
        {
            expectedCommand = null;
            result = false;
        }

        public CommandValidatorStub(bool result, string expectedCommand)
        {
            this.result = result;
            this.expectedCommand = expectedCommand;
        }

        public bool Validate(string command){
            if (command is not null && command != expectedCommand)
                throw new Exception("invalid command");
            
            return result;
        }
    }
}

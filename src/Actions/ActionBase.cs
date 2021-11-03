using System;
using System.Threading.Tasks;
using Spectre.Console;

namespace InteractiveCLI.Actions
{
    public abstract class ActionBase
    {
        public abstract Task Run();
        
        protected async Task SelectOption(Func<Task> beforeOption, Func<string, Func<Task<bool>>> selectAction, Func<Task> afterOptions = null)
        {
            for (;;)
            {
                if (beforeOption is not null)
                    await beforeOption();
                
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices("Sub-Action", "Action", "Back", "Quit", "Help"));

                if (option is "Back" or "Quit")
                    break;

                if (option is "Help")
                {
                    PrintHelp();
                    continue;
                }

                var action = selectAction(option);

                var goBackOnCompletion = await action.Invoke();
                
                if (goBackOnCompletion)
                    break;
                
                if (afterOptions is null)
                    continue;

                await afterOptions();
            }
        }

        private void PrintHelp()
        {
            
        }
    }
}
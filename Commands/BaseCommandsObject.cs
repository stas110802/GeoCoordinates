using GeoCoordinates.Utilities;

namespace GeoCoordinates.Commands
{
    internal abstract class BaseCommandsObject
    {
        protected BaseCommandsObject()
        {
            InitVoidCommands();
            InitAsyncCommands();
        }

        public abstract void PrintCommands();

        public async Task ReadActionCommandKey()
        {
            var key = new ConsoleKey();
            while (key != ConsoleKey.Q)
            {
                key = Console.ReadKey(true).Key;
                await InvokeActionCommand(key);
            }
        }

        private void InitVoidCommands()
        {
            var type = GetType();
            if (this == null) throw new ArgumentNullException(type.Name);
            VoidCommands = CommandHelper.GetConsoleCommands<Action>(this, type);
        }

        private void InitAsyncCommands()
        {
            var type = GetType();
            if (this == null) throw new ArgumentNullException(type.Name);
            AsyncCommands = CommandHelper.GetConsoleCommands<Func<Task>>(this, type);
        }

        private Dictionary<ConsoleKey, Action>? VoidCommands { get; set; }
        private Dictionary<ConsoleKey, Func<Task>>? AsyncCommands { get; set; }

        private async Task InvokeActionCommand(ConsoleKey key)
        {
            if (VoidCommands != null || AsyncCommands != null)
            { 
                var isSync = InvokeSyncMethodByKey(key);
                if(isSync is false)
                {
                    await InvokeAsyncMethodByKey(key);
                }            
            }
            else
            {
                throw new NullReferenceException($"{nameof(VoidCommands)} uninitialized");
            }              
        }

        private bool InvokeSyncMethodByKey(ConsoleKey key)
        {
            var action = VoidCommands!.ContainsKey(key) ? VoidCommands[key] : null;
            if (action == null) return false;

            action.Invoke();
            PrintCommands();

            return true;
        }

        private async Task InvokeAsyncMethodByKey(ConsoleKey key)
        {
            var action = AsyncCommands!.ContainsKey(key) ? AsyncCommands[key] : null;
            if (action == null) return;

            await action.Invoke();
            PrintCommands();
        }
    }
}

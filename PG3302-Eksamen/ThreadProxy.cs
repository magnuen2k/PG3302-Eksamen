using System.Threading;

namespace PG3302_Eksamen
{
    public abstract class ThreadProxy {
        private readonly Thread _thread;
        private bool _running;

        public Thread Thread { get => _thread; }
        public bool IsAlive { get => _thread.IsAlive; }
        public bool Running { get => _running; }

        protected ThreadProxy () {
            _thread = new Thread(new ThreadStart(ThreadLoop));
            _running = false;
        }

        protected abstract void Play ();

        private void ThreadLoop () {
            while(_running) {
                Play();
            }
        }

        public void Start() {
            _running = true;
            _thread.Start();
            while (!_thread.IsAlive) ;
        }

        protected void Stop() {
            _running = false;
            _thread.Join();
        }
    }
}
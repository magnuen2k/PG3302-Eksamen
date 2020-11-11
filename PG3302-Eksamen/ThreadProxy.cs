using System.Threading;

namespace PG3302_Eksamen
{
    abstract class ThreadProxy {

        protected Thread _thread;
        protected bool _running;

        public Thread Thread { get => _thread; }
        public bool IsAlive { get => _thread.IsAlive; }
        public bool Running { get => _running; }

        public ThreadProxy () {
            _thread = new Thread(new ThreadStart(ThreadLoop));
            _running = false;
        }

        protected abstract void Play ();

        protected void ThreadLoop () {
            while(_running) {
                Play();
            }
        }

        public void Start() {
            _running = true;
            _thread.Start();
            while (!_thread.IsAlive) ;
        }

        public void Stop() {
            _running = false;
            _thread.Join();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataflowSample
{
    internal class CommonDataPipeline<T>
    {
        private List<Action<T>> _actions;

        private PipelineContext<T> _context;
        public CommonDataPipeline()
        {
            this._actions = new List<Action<T>>();
        }

        public CommonDataPipeline(object param) : this()
        {
            this._context = new PipelineContext<T>(_actions,param);
        }

        public void AddAction(Action<T> action)
        {
            _actions.Add(action);
        }

        public virtual void Invoke()
        {
            _context?.InvokeNext();
        }

        public virtual async Task InvokeAsync()
        {
            await _context?.InvokeNextAsync();
        }

        public Dictionary<string,object> Result
        {
            get
            {
                return this._context.OutputData;
            }
        }
    }


    internal class PipelineContext<T>
    {
        private readonly IEnumerable<Action<T>> _actions;
        public Action<T> CurrentAction { get; set; }
        public T InputData { get; set; }

        public Dictionary<string, object> OutputData { get; set; }

        public PipelineContext(List<Action<T>> actions, object param)
        {
            this._actions = actions;    
            this.InputData =(T)param;
            this.OutputData = new Dictionary<string, object>();
        }

        public void InvokeNext()
        {
            foreach (var action in _actions)
            {
                CurrentAction = action;
                action?.Invoke(InputData);
            }
        }

        public async Task InvokeNextAsync()
        {
            foreach (var item in _actions)
            {
               
              await   Task.Run(() => item.Invoke(InputData));
            } 
        }
    }
}

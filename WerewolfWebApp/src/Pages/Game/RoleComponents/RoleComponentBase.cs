using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WerewolfWebApp.Hubs;
using WerewolfWebApp.Model;
using WerewolfWebApp.Pages.Game.Shared;

namespace WerewolfWebApp.Pages.Game.RoleComponents {
	public abstract class RoleComponentBase : ComponentBase, IComponent {

        [Parameter]
        public string _playerName { get; set; }
        [Parameter]
        public int _gameId { get; set; }
        [Parameter]
        public RolePage Parent { get; set; }

        protected bool dead = false;
        protected List<string> events = new List<string>();
        protected HubConnection _hubConnection;
        protected RoleComponent roleComponent;
        protected abstract NavigationManager _NavigationManager { get; }
        protected abstract RoleName _Role { get; }

        protected override async Task OnInitializedAsync() {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_NavigationManager.ToAbsoluteUri("/playerhub"))
                .WithAutomaticReconnect()
                .Build();
            SetListeners();
            SetRoleListeners();
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync<bool>(PlayerHubMethod.Relog, _playerName, _gameId, _Role);
        }
        protected override void OnAfterRender(bool firstRender) {
            if (firstRender) {
                roleComponent.pollComponent._hubConnection = _hubConnection;
                roleComponent.pollComponent.gameId = _gameId;
            }
            base.OnAfterRender(firstRender);
        }
        protected abstract void SetRoleListeners();

        private void SetListeners() {

            _hubConnection.On<PollType, List<string>, int>(PlayerClientMethod.ReceivePoll, (type, nominees, timer) => {
                Parent.Timer = (timer / 1000) + 1;
                Console.WriteLine("poll received: " + type.ToString() + ", " + nominees.First());
                roleComponent.pollComponent.SetPoll(type, nominees);
                StateHasChanged();
            });
            _hubConnection.On(PlayerClientMethod.PollClosed, () => {
                Parent.Timer = 0;
                Console.WriteLine("poll closed");
                roleComponent.pollComponent.Hidden = true;
                StateHasChanged();
            });
            _hubConnection.On<string>(PlayerClientMethod.ReceiveEvent, (Event) => {
                events.Insert(0, Event);
                StateHasChanged();
            });
        }
    }
}

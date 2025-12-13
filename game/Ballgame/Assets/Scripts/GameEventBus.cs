using System;
using JUtils;
using Sockets;


public class GameEventBus : SingletonBehaviour<GameEventBus> {

    public Action<GameOptionDto> PlayerFinished;

}
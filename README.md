## vJoyDiscreteThrottle

> [!IMPORTANT]
> This program is bare-bones at this stage.

This is a simple HTTP server that creates and manages a virtual, discrete-stepped throttle as a vJoy device.

* **Precise Control**: No more mashing the keyboard up and down to hit a specific throttle value.
* **Discrete Notches**: Inspired by Japanese train (simulators), this server brings discrete throttle notches to European-style smooth control levers, bridging the gap for a unique driving experience.

### API Endpoint

All endpoints are on the `/api/throttle` route.

| Type | Route | Effect |
|------|-------|--------|
| GET | / | Get current throttle level |
| GET | /normalized | Get current throttle level in [0, 32767] |
| POST | /increment | Increase the throttle by a discrete step |
| POST | /decrement | The opposite |
| POST | /neutral | Reset the throttle to neutral |

### Acknowledgements

- [shauleiz/vJoy](https://github.com/shauleiz/vJoy/)
- [vJoy + C# を用いて、ゲームパッド入力をシミュレートする方法](https://qiita.com/Limitex/items/23faaf3a0ef6ca8832e1)
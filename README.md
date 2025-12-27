## vJoyDiscreteThrottle

A simple HTTP server that manages a virtual, discrete-stepped throttle as a vJoy device.

This program is useful when train simulators use continuous throttle input (like the [Flexity Swift](https://en.wikipedia.org/wiki/Flexity_Swift)),
but you want to control the throttle in discrete steps (like many Japanese trains).

The major difficulty with continuous throttles is accurately controlling them using a keyboard. It's fairly easy to under/overshoot the desired throttle setting. This program maps the continuous throttle to discrete steps, so you can accurately control the train's power settings.

---

### API Endpoint

All endpoints are on the `/api/v1` route.

| Method | Route | Behavior |
|--------|-------|----------|
| GET | /throttle | Return current throttle level |
| GET | /throttle/scaled | Return current throttle level scaled to [0, 32767] |
| POST | /notch/increment | Increase throttle by a discrete step <br> (<kbd>Z</kbd> key in OpenBVE/BVE Trainsim) |
| POST | /notch/decrement | Decrease throttle by a discerte step <br> (<kbd>Q</kbd> key in OpenBVE/BVE Trainsim) |
| POST | /notch/neutral/toward | Move throttle one notch toward neutral <br> (<kbd>A</kbd> in OpenBVE/BVE Trainsim)
| POST | /notch/neutral | Set throttle to neutral |
| POST | /notch/min | Set throttle to minimum |
| POST | /notch/max | Set throttle to maximum |


### Acknowledgements

- [shauleiz/vJoy](https://github.com/shauleiz/vJoy/)
- [vJoy + C# を用いて、ゲームパッド入力をシミュレートする方法](https://qiita.com/Limitex/items/23faaf3a0ef6ca8832e1)
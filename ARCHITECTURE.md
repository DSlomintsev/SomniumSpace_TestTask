What would need to change to support 100 concurrent users

I have 2 ideas:
1. Use Photon Quantum 3(it uses ECS which looks very optimised, one more thing client side will use ECS too, and will gives good performance and more optimal maintenance costs).
2. Create custom dedicates server which will use WebSocket for communication(client-backedn) and UDP Broadcast or Multicast server to share states for all connected users.

One more thing we should pay attention to client side. In case 100 users in one room, need CPU RAM and GPU optimization.
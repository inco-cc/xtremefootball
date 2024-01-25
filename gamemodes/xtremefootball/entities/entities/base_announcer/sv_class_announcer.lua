local Announcer = debug.getregistry().Announcer

util.AddNetworkString("Announcer:Say")

function Announcer:Say(phrase, filter)
	net.Start("Announcer:Say")
	net.WriteEntity(self)
	net.WriteString(phrase)

	if filter then
		net.Send(filter)
	else
		net.Broadcast()
	end
end

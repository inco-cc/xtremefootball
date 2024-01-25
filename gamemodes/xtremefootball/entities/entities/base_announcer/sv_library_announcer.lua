module("announcer", package.seeall)

concommand.Add("xf_announcer_say", function(player, command, arguments)
	if not player:IsValid() or player:IsAdmin() then
		Say(unpack(arguments))
	end
end)

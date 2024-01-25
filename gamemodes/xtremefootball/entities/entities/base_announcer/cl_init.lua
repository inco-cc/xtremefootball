include("shared.lua")

function ENT:Initialize()
	debug.setmetatable(self, debug.getregistry().Announcer)
end

AddCSLuaFile("cl_init.lua")

include("shared.lua")

function ENT:Initialize()
	debug.setmetatable(self, debug.getregistry().Announcer)

	announcer.Set(self)
end

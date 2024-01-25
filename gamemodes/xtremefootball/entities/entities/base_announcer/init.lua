AddCSLuaFile("cl_init.lua")

include("shared.lua")

function ENT:Initialize()
	debug.setmetatable(self, debug.getregistry().Announcer)

	announcer.Set(self)

	if not self:CreatedByMap() then
		self:AddEFlags(EFL_KEEP_ON_RECREATE_ENTITIES)
	end

	self:AddEFlags(EFL_FORCE_CHECK_TRANSMIT)
	self:AddEFlags(EFL_NO_THINK_FUNCTION)
	self:AddEFlags(EFL_NO_GAME_PHYSICS_SIMULATION)
	self:AddEFlags(EFL_DONTBLOCKLOS)
	self:AddEFlags(EFL_DONTWALKON)
	self:AddEFlags(EFL_NO_PHYSCANNON_INTERACTION)
end

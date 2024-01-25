include("shared.lua")

function ENT:Initialize()
	debug.setmetatable(self, debug.getregistry().Announcer)

	announcer.Set(self)

	self:AddEffects(EF_NOINTERP)
	self:AddEffects(EF_NOSHADOW)
	self:AddEffects(EF_NODRAW)
	self:AddEffects(EF_NORECEIVESHADOW)
	self:AddEffects(EF_NOFLASHLIGHT)
end

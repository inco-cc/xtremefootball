include("shared.lua")
include("cl_class_announcer.lua")

function ENT:Initialize()
	debug.setmetatable(self, debug.getregistry().Announcer)

	announcer.Set(self)

	self:AddEffects(EF_NOINTERP)
	self:AddEffects(EF_NOSHADOW)
	self:AddEffects(EF_NODRAW)
	self:AddEffects(EF_NORECEIVESHADOW)
	self:AddEffects(EF_NOFLASHLIGHT)
end

function ENT:GetPhraseSound(phrase)
	local sound = self.PhraseSound

	if not isstring(sound) then
		sound = string.format("%s.%s",
		    self:GetClass():Replace("_", "."), phrase)
	end

	return sound:format(phrase)
end

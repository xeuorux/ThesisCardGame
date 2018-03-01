﻿public class CreatureCardDefinition : SpellCardDefinition
{
	public int Power
	{
		get
		{
			return power;
		}
	}
	protected int power;

	public int Toughness
	{
		get
		{
			return toughness;
		}
	}
	protected int toughness;

	public CreatureCardDefinition(string cardName, int manaCost, int power, int toughness) : base(cardName, manaCost)
	{
		this.power = power;
		this.toughness = toughness;
	}

	public override Card GetCardInstance()
	{
		return new CreatureCard(this);
	}
}
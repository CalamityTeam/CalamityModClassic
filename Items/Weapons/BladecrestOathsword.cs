using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BladecrestOathsword : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/BladecrestOathsword");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Bladecrest Oathsword");
		Item.width = 50;  //The width of the .png file in pixels divided by 2.
		Item.damage = 32;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useStyle = 1;
		Item.useTime = 25;
		////Tooltip.SetDefault("Sword of an ancient demon lord");
		Item.knockBack = 4f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 100000;  //Value is calculated in copper coins.
		Item.rare = 3;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BloodScythe").Type;
		Item.shootSpeed = 6f;
	}

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 200);
	}
}}

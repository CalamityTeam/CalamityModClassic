using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CrushsawCrasher : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/6.Calamitas/CrushsawCrasher");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Crushsaw Crasher");
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 52;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 21;
		Item.useStyle = 1;
		Item.useTime = 21;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 22;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 500000;  //Value is calculated in copper coins.
		Item.rare = 6;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Crushax").Type;
		Item.shootSpeed = 11f;
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
	}
}}

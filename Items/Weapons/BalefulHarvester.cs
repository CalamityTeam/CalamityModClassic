using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BalefulHarvester : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/BalefulHarvester");
		return true;
	}
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Baleful Harvester");
		Item.damage = 123;
		Item.width = 54;
		Item.height = 54;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.useAnimation = 32;
		Item.useStyle = 1;
		Item.useTime = 32;
		Item.useTurn = true;
		Item.knockBack = 8;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.maxStack = 1;
		Item.value = 475000;
		Item.rare = 7;
		Item.shoot = Mod.Find<ModProjectile>("BalefulHarvesterProjectile").Type;
		Item.shootSpeed = 6f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Pumpkin, 30);
		recipe.AddIngredient(ItemID.BookofSkulls);
        recipe.AddIngredient(ItemID.SpookyWood, 200);
        recipe.AddIngredient(ItemID.TheHorsemansBlade);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 2400);
	}
}}

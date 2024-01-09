using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SpearofPaleolith : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Spear of Paleolith");
		//Tooltip.SetDefault("Throws an ancient spear that shatters enemy armor at high velocity\nSpears rain fossil shards as they travel");
	}

	public override void SetDefaults()
	{
		Item.width = 54;
		Item.damage = 52;
		Item.DamageType = DamageClass.Throwing;
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 27;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 27;
		Item.knockBack = 6f;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.height = 54;
		Item.maxStack = 1;
		Item.value = 330000;
		Item.rare = ItemRarityID.Pink;
		Item.shoot = Mod.Find<ModProjectile>("SpearofPaleolith").Type;
		Item.shootSpeed = 35f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
		recipe.AddIngredient(ItemID.AdamantiteBar, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
		recipe.AddIngredient(ItemID.TitaniumBar, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}

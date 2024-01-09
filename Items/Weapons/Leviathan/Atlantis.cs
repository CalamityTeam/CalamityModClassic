using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Leviathan {
public class Atlantis : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Atlantis");
		//Tooltip.SetDefault("Casts aquatic spears");
	}

    public override void SetDefaults()
    {
        Item.damage = 45;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 12;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5f;
        Item.value = 600000;
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item34;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("AtlantisSpear").Type;
        Item.shootSpeed = 32f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "IOU");
        recipe.AddIngredient(null, "LivingShard");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}
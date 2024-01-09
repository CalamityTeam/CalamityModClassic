using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TheBallista : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Ballista");
		}

    public override void SetDefaults()
    {
        Item.damage = 110;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 40;
        Item.height = 62;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 8f;
        Item.value = 6000000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BallistaGreatArrow").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 20f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BallistaGreatArrow").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Marrow);
        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
        recipe.AddIngredient(ItemID.Ectoplasm, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}